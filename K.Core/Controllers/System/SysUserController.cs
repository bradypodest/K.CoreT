using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K.Core.Common.HttpContextUser;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.System
{
    [Route("api/Sys_User")]
    public class SysUserController : BaseController<SysUser,ISysUserService>
    {
        public SysUserController(ISysUserService service,IUser httpUser)
        : base( service, httpUser)
        {

        }
    }
}