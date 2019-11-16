using K.Core.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.Filter
{
    public class GlobalActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //判断权限
           // if (context.HttpContext.User.Identity.IsAuthenticated)
           //{
                //var roleType = int.Parse(context.HttpContext.User.Claims.First(c => c.Type == "roleType").Value);
                ////不是管理人员
                //if (roleType <= 0 || roleType >= 4)
                //{
                //    context.Result = new JsonResult(new Result(214));
                //}



                //校验数据
                if (context.ModelState.IsValid == false)
                {
                    var result = MessageModel<bool>.Fail(context.ModelState.GetAllErrMsgStr(","),ResponseEnum.ServerRequestError);
                    //result.message = actionContext.ModelState.GetAllErrMsgStr(",");
                    //actionContext.Response = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.GetEncoding("UTF-8"), "application/json") };

                    //context.Result = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.GetEncoding("UTF-8"), "application/json") };
                    context.Result = new JsonResult(result);

                }

            //}
            //else
            //    context.Result = new JsonResult(new Result(214));
            


           
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    if (actionContext.ModelState.IsValid == false)
        //    {
        //        var result = ResultData.Error(EnumAppCode.InternalServer);
        //        result.message = actionContext.ModelState.GetAllErrMsgStr(",");
        //        actionContext.Response = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.GetEncoding("UTF-8"), "application/json") };
        //    }
        //}
    }
}
