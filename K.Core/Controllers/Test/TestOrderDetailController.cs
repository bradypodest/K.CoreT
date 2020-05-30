using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Controllers.Base;
using K.Core.Model.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using K.Core.IServices.Test;
using K.Core.Common.Model;
using K.Core.Common.Helper;

namespace K.Core.Controllers.Test
{
    /// <summary>
    /// 订单测试
    /// </summary>
    [Route("api/TestOrderDetail")]
    public class TestOrderDetailController : BaseController<TestOrderDetail, TestOrderDetail, ITestOrderDetailService>
    {
        readonly ITestOrderDetailService _testOrderDetailServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public TestOrderDetailController(ITestOrderDetailService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _testOrderDetailServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法
        
        #endregion
    }
}
