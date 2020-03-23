using AutoMapper;
using K.Core.AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model.Models;
using K.Core.Model.ViewModels.System;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.Services.System
{
    class SysRoleMenuPowerGService : BaseServices<SysRoleMenuPowerGroup>, ISysRoleMenuPowerGService
    {
        ISysRoleMenuPowerGRepository _dal;
        ISysRoleRepository _sysRoleRepository;
        IMapper _mapper;
        IUser _httpUser;
        public SysRoleMenuPowerGService(ISysRoleMenuPowerGRepository dal, IMapper mapper, IUser httpUser,ISysRoleRepository sysRoleRepository)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;

            _sysRoleRepository = sysRoleRepository;
        }

        /// <summary>
        /// 获取角色对应菜单的权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public async Task<MessageModel<List<SysRoleMenuPowerGVM>>> GetRoleMenuPowerG(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId)) 
            {
                return MessageModel<List<SysRoleMenuPowerGVM>>.Fail("未传入角色ID");
            }

            //查询角色是否存在
            var sysRole = await _sysRoleRepository.QueryById(roleId);
            if (sysRole != null && sysRole.Status == Model.StatusE.Live)
            {
                //查询角色对应菜单的权限组
                var sysRoleMenuPowerGs = await _dal.Query(m => m.RoleID == roleId && m.Status == Model.StatusE.Live);

                //将 SysRoleMenuPowerG  转为 sysRoleMenuPowerGVM
                var source = new Source<List<SysRoleMenuPowerGroup>> { Value = sysRoleMenuPowerGs };
                var t = _mapper.Map<Destination<List<SysRoleMenuPowerGVM>>>(source);
                var returnSysRoleMenuPowerGroupVM = t.Value;

                return MessageModel<List<SysRoleMenuPowerGVM>>.Success(returnSysRoleMenuPowerGroupVM);
            }
            else 
            {
                return MessageModel<List<SysRoleMenuPowerGVM>>.Fail("菜单已不存在");
            }


        }

        /// <summary>
        /// 更新角色对应菜单权限
        /// </summary>
        /// <param name="sysRoleMenuPowerGVMs"></param>
        /// <returns></returns>
        public async Task<MessageModel<bool>> UpdateRoleMenuPowerGs(List<SysRoleMenuPowerGVM> sysRoleMenuPowerGVMs)
        {
            var roleID = sysRoleMenuPowerGVMs?.FirstOrDefault().RoleID;//找到对应的角色


            #region //条件判断      （先不考虑用抽象方法方法）
            var sysRole = await _sysRoleRepository.QueryById(roleID);
            if (sysRole == null || sysRole.Status == Model.StatusE.Delete) 
            {
                return MessageModel<bool>.Fail("角色已不存在");
            }

            #endregion

            #region //数据准备
            //因为后台数据库的数据都需要新增人
            sysRoleMenuPowerGVMs.ForEach(d=> {
                d.ID = Guid.NewGuid().ToString();
                d.CreateID = _httpUser.ID;
                d.CreateTime = DateTime.Now;
                d.Creator = _httpUser.Name;


            });
            #endregion

            #region //开启事务
            //Action 是封装一个方法，该方法不具有参数且不返回值。 https://docs.microsoft.com/zh-cn/dotnet/api/system.action?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DZH-CN%26k%3Dk(System.Action);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
            //Func<TResult> 委托  封装一个不具有参数但却返回 TResult 参数指定的类型值的方法。
            var tranResult = _dal.UseTran(async ()=> 
            {
                //查询到之前的角色 权限数据
                var sysRoleMenuPowerGs = await _dal.Query(m=> m.RoleID== roleID && m.Status == Model.StatusE.Live);

                //删除之前对应的角色的权限数据
                await _dal.DeleteByIds(sysRoleMenuPowerGs.Select(d=>d.ID).ToArray());


                //将 sysRoleMenuPowerGVM 转为 SysRoleMenuPowerG
                var source = new Source<List<SysRoleMenuPowerGVM>> { Value = sysRoleMenuPowerGVMs };
                var t = _mapper.Map<Destination<List<SysRoleMenuPowerGroup>>>(source);
                var sysRoleMenuPowerGroupsU = t.Value;


                //新增对应的角色的菜单权限组
                try
                {
                    await _dal.Add(sysRoleMenuPowerGroupsU);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ;
                }

            });
            #endregion

            if (tranResult)
            {
                return MessageModel<bool>.Success(true, "更新成功");
            }

            return MessageModel<bool>.Fail("事务出错，请重试");

        }
    }
}
