using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.System
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/Sys_User")]
    public class SysUserController : BaseController<SysUser,ISysUserService>
    {
        readonly ISysUserService _sysUserServices;
        private readonly IUser _user;

        public SysUserController(ISysUserService service,IUser httpUser)
        : base( service, httpUser)
        {
            _sysUserServices = service;
            _user = httpUser;
        }

        #region  重写baseController 方法

        #endregion

        #region  其他方法

        


        #endregion



    }
}