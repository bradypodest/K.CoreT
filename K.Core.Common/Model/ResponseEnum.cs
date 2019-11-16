using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace K.Core.Common.Model
{
    public enum ResponseEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Success =20000,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermissions = 40001,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("找不到指定资源")]
        NoFound = 40004,
        /// <summary>
        /// 服务器错误
        /// </summary>
        [Description("服务器错误")]
        ServerError = 50000,
        /// <summary>
        /// 请求失败
        /// </summary>
        [Description("请求失败")]
        ServerRequestError = 50001,
    }
}
