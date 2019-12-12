using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model;
using K.Core.Model.Models;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.Services.System
{
    public class SysMenuPowerGService: BaseServices<SysMenuPowerGroup>, ISysMenuPowerGService
    {
        ISysMenuPowerGRepository _dal;
        ISysMenuRepository _sysMenuRepository;
        ISysPowerRepository _sysPowerRepository;
        IMapper _mapper;
        IUser _httpUser;
        public SysMenuPowerGService(ISysMenuPowerGRepository dal, IMapper mapper, IUser httpUser, ISysMenuRepository sysMenuRepository, ISysPowerRepository sysPowerRepository)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;

            _sysMenuRepository = sysMenuRepository;
            _sysPowerRepository = sysPowerRepository;
        }


        #region 重写baseservice方法

        #endregion

        #region ISysMenuPowerGservice 实现方法
        public async Task<MessageModel<List<SysMenuPowerGroup>>> GetMenuPowerGroups(string menuId) 
        {
            if (string.IsNullOrWhiteSpace(menuId)) 
            {
                return MessageModel<List<SysMenuPowerGroup>>.Fail("未传入菜单ID");
            }

            //查询菜单是否存在
            var sysMenu = await _sysMenuRepository.QueryById(menuId);
            if (sysMenu != null && sysMenu.Status != StatusE.Delete) 
            {
                //查询菜单的权限组
                var sysMenuPowerGroups = _dal.Query(d => d.SysMenuID == menuId && d.Status == StatusE.Live);

                //var sysPowers = _sysPowerRepository.Query(d=>d.Status == StatusE.Live);

                //var sysPowers= _sysPowerRepository.QueryByIDs()
            }

            return MessageModel<List<SysMenuPowerGroup>>.Fail("菜单已不存在");

        }
        #endregion 
    }
}
