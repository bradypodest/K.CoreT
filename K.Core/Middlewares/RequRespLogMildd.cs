using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using K.Core.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Builder;
using System.IO;
using K.Core.Common.LogHelper;
using StackExchange.Profiling;
using System.Text.RegularExpressions;
using K.Core.IServices;
using K.Core.IServices.System;

namespace K.Core.Middlewares
{
    /// <summary>
    /// 中间件
    /// 记录请求和响应数据
    /// </summary>
    public class RequRespLogMildd
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        private readonly ISysUserService _sysUserService;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="next"></param>
        ///// <param name="blogArticleServices"></param>
        //public RequRespLogMildd(RequestDelegate next)
        //{
        //    _next = next;
        //}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="sysUserService"></param>
        public RequRespLogMildd(RequestDelegate next, ISysUserService sysUserService)
        {
            _next = next;
            _sysUserService = sysUserService;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            // 过滤，只有接口
            if (context.Request.Path.Value.Contains("api"))
            {
                context.Request.EnableBuffering();
                Stream originalBody = context.Response.Body;

                try
                {
                    // 存储请求数据
                    RequestDataLog(context.Request);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await _next(context);//下一个中间件 执行     //有点类似与 invocation.Proceed();

                        // 存储响应数据
                        ResponseDataLog(context.Response, ms);

                        ms.Position = 0;
                        await ms.CopyToAsync(originalBody);
                    }
                }
                catch (Exception)
                {
                    // 记录异常
                    //ErrorLogData(context.Response, ex);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 记录下请求的路径和参数
        /// </summary>
        /// <param name="request"></param>
        private void RequestDataLog(HttpRequest request)
        {
            var sr = new StreamReader(request.Body);

            var content = $" QueryData:{request.Path + request.QueryString}\r\n BodyData:{sr.ReadToEnd()}";

            if (!string.IsNullOrEmpty(content))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Request Data:", content });

                });

                request.Body.Position = 0;
            }

        }

        /// <summary>
        /// 写入log:返回的参数
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ms"></param>
        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var ResponseBody = new StreamReader(ms).ReadToEnd();

            // 去除 Html
            var reg = "<[^>]+>";
            var isHtml = Regex.IsMatch(ResponseBody, reg);

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });

                });
            }
        }


        //private readonly ISysUserService _sysUserService;

        //IMyScopedService is injected into Invoke
        //public async Task Invoke(HttpContext httpContext, ISysUserService sysUserService)
        //{
        //    //sysUserService. = 1000;
        //    await _next(httpContext);
        //}
    }
}

