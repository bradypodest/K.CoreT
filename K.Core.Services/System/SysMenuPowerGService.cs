using AutoMapper;
using K.Core.AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model;
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
    public class SysMenuPowerGService: BaseServices<SysMenuPowerGroup>, ISysMenuPowerGService
    {
        ISysMenuPowerGRepository _dal;
        ISysMenuRepository _sysMenuRepository;
        ISysPowerRepository _sysPowerRepository;
        ISysPowerGroupRepository _sysPowerGroupRepository;
        IMapper _mapper;
        IUser _httpUser;
        public SysMenuPowerGService(ISysMenuPowerGRepository dal, IMapper mapper, IUser httpUser, ISysMenuRepository sysMenuRepository, ISysPowerRepository sysPowerRepository, ISysPowerGroupRepository sysPowerGroupRepository)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;

            _sysMenuRepository = sysMenuRepository;
            _sysPowerRepository = sysPowerRepository;
            _sysPowerGroupRepository = sysPowerGroupRepository;
        }


        #region 重写baseservice方法

        #endregion

        #region ISysMenuPowerGservice 实现方法
        /// <summary>
        /// 获取菜单权限   待优化代码
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<SysMenuPowerGroupVM>>> GetMenuPowerGroups(string menuId) 
        {
            if (string.IsNullOrWhiteSpace(menuId)) 
            {
                return MessageModel<List<SysMenuPowerGroupVM>>.Fail("未传入菜单ID");
            }

            //查询菜单是否存在
            var sysMenu = await _sysMenuRepository.QueryById(menuId);
            if (sysMenu != null && sysMenu.Status != StatusE.Delete) 
            {
                //查询菜单的权限组
                var sysMenuPowerGroups = await _dal.Query(d => d.SysMenuID == menuId && d.Status == StatusE.Live);

                //var resultR=from sysMenuPowersG in sysMenuPowerGroups.sys
                

                var arrayPowerGroups = sysMenuPowerGroups.Select(s => s.SysPowerGroupID).ToArray();

                var sysPowers = await _sysPowerRepository.Query(d => (arrayPowerGroups).Contains(d.SysPowerGroupID) && d.Status == StatusE.Live);

                var returnMenuPowerGroupsVM = new List<SysMenuPowerGroupVM>();
                foreach (var item in sysMenuPowerGroups)
                {
                    //var sysMenuPowerGroupVM = new SysMenuPowerGroupVM();

                    //mapper
                    var source = new Source<SysMenuPowerGroup> { Value = item };
                    var sysMenuPowerGroupVM = _mapper.Map<Destination<SysMenuPowerGroupVM>>(source).Value;
                    


                    sysMenuPowerGroupVM.SysMenu = sysMenu;
                    sysMenuPowerGroupVM.SysPowers = sysPowers.Where(d => d.SysPowerGroupID == item.SysPowerGroupID).ToList();

                    sysMenuPowerGroupVM.SysPowerGroup = await _sysPowerGroupRepository.QueryById(item.SysPowerGroupID);

                    returnMenuPowerGroupsVM.Add(sysMenuPowerGroupVM);

                }

                return MessageModel<List<SysMenuPowerGroupVM>>.Success(returnMenuPowerGroupsVM);

            }

            return MessageModel<List<SysMenuPowerGroupVM>>.Fail("菜单已不存在");

        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="sysMenuPowerGVMs"></param>
        /// <returns></returns>
        public async Task<MessageModel<bool>> UpdateMenuPowerGroups(List<SysMenuPowerGroupVM> sysMenuPowerGVMs)
        {
            var menuID = sysMenuPowerGVMs?.FirstOrDefault().SysMenuID;

            //判断对应的菜单是否存在
            var sysMenu = await _sysMenuRepository.QueryById(menuID);
            if (sysMenu == null || sysMenu.Status == StatusE.Delete) 
            {
                return MessageModel<bool>.Fail("菜单已不存在");
            }

            //开启事务
            var tranResult= await _dal.UseTranAsync(async () =>
             {
                 //查找到对应的菜单-权限组
                 var sysMenuPowerGroups = await _dal.Query(m => m.SysMenuID == sysMenu.ID&&m.Status==StatusE.Live);
                 //删除对应菜单的权限组
                 await _dal.DeleteByIds(sysMenuPowerGroups.Select(d=>d.ID).ToArray());

                 //查询对应的权限组对应的权限 且删除
                 await _sysPowerRepository.DeleteByIds((await _sysPowerRepository.Query(m => (sysMenuPowerGroups.Select(d=>d.SysPowerGroupID).ToArray() )
                                                        .Contains( m.SysPowerGroupID )  
                                                        && m.Status == StatusE.Live))
                                                        .Select(d => d.ID).ToArray());


                 //将 SysMenuPowerGroupVM 转为 SysMenuPowerGroup
                 //将数据转化为T
                 var source = new Source<List<SysMenuPowerGroupVM>> { Value = sysMenuPowerGVMs };
                 var t = _mapper.Map<Destination<List<SysMenuPowerGroup>>>(source);
                 var sysMenuPowerGroupsU = t.Value;



                 //新增对应的权限组
                 await _dal.Add(sysMenuPowerGroupsU);

                 //新增权限组
                 await _sysPowerGroupRepository.Add(sysMenuPowerGVMs.Select(d=>d.SysPowerGroup).ToList());

                 //组合所有的权限
                 var sysPowers = new List<SysPower>();
                 foreach (var item in sysMenuPowerGVMs)
                 {
                     //sysPowers.Add(item.SysPowers);
                     sysPowers.Union(item.SysPowers).ToList<SysPower>();
                 }

                 //新增权限组对应的权限
                 await _sysPowerRepository.Add(sysPowers);

             });

            if (tranResult) 
            {
                return MessageModel<bool>.Success("更新成功");
            }

            return MessageModel<bool>.Fail("事务出错，请重试");
        }
        #endregion
    }
}
