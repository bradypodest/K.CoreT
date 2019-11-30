using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.AuthHelper.OverWrite;
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
    /// <summary>
    /// 菜单 
    /// </summary>
    [Route("api/SysMenu")]
    public class SysMenuController : BaseController<SysMenu, SysMenuVM, ISysMenuService>
    {
        readonly ISysMenuService _sysMenuServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public SysMenuController(ISysMenuService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _sysMenuServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMenuTree")]
        public async Task<MessageModel<SysMenuTreeVM>> GetMenuTree(string parentId ="")
        {
            return await _sysMenuServices.GetMenuTree(parentId);
        }

        /// <summary>
        /// 获取用户的菜单树
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserMenuTree")]
        public async Task<MessageModel<SysMenuTreeVM>> GetUserMenuTree(string token ) 
        {
            var data = MessageModel<SysMenuTreeVM>.Fail();

            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                if (tokenModel != null && !string.IsNullOrWhiteSpace(tokenModel.Uid))
                {
                    return await _sysMenuServices.GetUserMenuTree(tokenModel.Uid);
                }

            }

            return data;
        }

        #endregion
    }
}
