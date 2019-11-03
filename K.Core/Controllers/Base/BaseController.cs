using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using K.Core.Common.Helper;
using K.Core.IServices.BASE;
using K.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.Base
{
    public class BaseController <T,IEntityService>: Controller
        where T : class
        where IEntityService : IBaseServices<T>
    {
        /// <summary>
        /// 这个是对应实体类的 service
        /// </summary>
        protected IEntityService _service;
        public BaseController()
        {

        }
        public BaseController(IEntityService service)
        {
            _service = service;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("GetPageData")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<PageModel<T>>> GetPageData(PageDataOptions pageDataOptions)
        {
            MessageModel<PageModel<T>> resp = new MessageModel<PageModel<T>>();

            //检查传入参数是否有误 （未实现） 

            var oLamadaExtention = LambdaHelper.True<T>();

            resp.Data= await _service.QueryPage(oLamadaExtention, pageDataOptions);

            resp.Success = true;
            resp.Msg = "OK";

            return resp;
        }

    }
}