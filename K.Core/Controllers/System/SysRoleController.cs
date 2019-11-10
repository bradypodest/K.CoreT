using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.System
{
    [Route("api/SysRole")]
    public class SysRoleController  : BaseController<SysRole, SysRole, ISysRoleService>
    {
        readonly ISysRoleService _sysRoleServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public SysRoleController(ISysRoleService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _sysRoleServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法




        #endregion
    }
}