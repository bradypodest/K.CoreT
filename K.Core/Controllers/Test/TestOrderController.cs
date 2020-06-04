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
using K.Core.Model.ViewModels.Test;

namespace K.Core.Controllers.Test
{
    /// <summary>
    /// 订单测试
    /// </summary>
    [Route("api/TestOrder")]
    public class TestOrderController : BaseController<TestOrder, TestOrderVM, ITestOrderService>
    {
        readonly ITestOrderService _testOrderServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public TestOrderController(ITestOrderService service, IUser httpUser, IMapper mapper)
        : base(service, httpUser, mapper)
        {
            _testOrderServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法
        /// <summary>
        /// 获取订单的一个流水号
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetOneOrderNo")]
        public MessageModel<string> GetOneOrderNo()
        {

            //待处理，先使用简单的
            //return Task < MessageModel<string>$"DD{DateTime.Today.ToString("yyyyMMdd")}";
            //return MessageModel<string>.Success($"DD{DateTime.Today.ToString("yyyyMMddhhmmss")}", "OK");
            return MessageModel<string>.Success($"DD{DateTime.Now:yyyyMMddhhmmss}", "OK");
            //SerialNoHelper.Helper.Generate

        }
        #endregion
    }
}
