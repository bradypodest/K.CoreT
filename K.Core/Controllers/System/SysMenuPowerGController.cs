using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model;
using K.Core.Model.Models;
using K.Core.Model.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.System
{
    [Route("api/SysMenuPowerG")]
    public class SysMenuPowerGController : BaseController<SysMenuPowerGroup, SysMenuPowerGroup, ISysMenuPowerGService>
    {
        readonly ISysMenuPowerGService _sysMenuPowerGServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public SysMenuPowerGController(ISysMenuPowerGService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _sysMenuPowerGServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet,Route("GetMenuPowerGroups")]
        public async Task<MessageModel<List<SysMenuPowerGroupVM>>> GetMenuPowerGroups(string menuId) 
        {
            return await _sysMenuPowerGServices.GetMenuPowerGroups(menuId);
        }

        /// <summary>
        /// 更新菜单权限
        /// </summary>
        /// <param name="sysMenuPowerGVMs"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateMenuPowerGroups")]
        public async Task<MessageModel<bool>> UpdateMenuPowerGroups([FromBody]List<SysMenuPowerGroupVM> sysMenuPowerGVMs) 
        {
            return await _sysMenuPowerGServices.UpdateMenuPowerGroups(sysMenuPowerGVMs);
        }

        #endregion
    }
}
