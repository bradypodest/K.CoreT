﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace K.Core.Controllers.System
{
    /// <summary>
    /// 权限
    /// </summary>
    [Route("api/SysPower")]
    public class SysPowerController : BaseController<SysPower, SysPower, ISysPowerService>
    {
        readonly ISysPowerService _sysPowerServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public SysPowerController(ISysPowerService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _sysPowerServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法




        #endregion
    }
}
