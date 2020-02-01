using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Common.Model
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int code { get; set; } = 20000;

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = false;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; } = "服务器异常";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T data { get; set; }


        //public static MessageModel<T> Fail(string msgString="服务器异常") {
        //    return new MessageModel<T>()
        //    {
        //        code= (int)ResponseEnum.ServerRequestError,
        //        success = false,
        //        msg = msgString,
        //        data = default(T),
        //    };
        //}

        public static MessageModel<T> Fail(string msgString = "请求失败", ResponseEnum codeEnum = ResponseEnum.ServerRequestError)
        {
            return new MessageModel<T>()
            {
                code = (int)codeEnum,
                success = false,
                msg = msgString,
                data = default(T),
            };
        }


        public static MessageModel<T> Fail(T obj ,string msgString= "服务器异常")
        {
            return new MessageModel<T>()
            {
                code = (int)ResponseEnum.ServerRequestError,
                success = false,
                msg = msgString,
                data = obj,
            };
        }

        public static MessageModel<T> Fail(T obj, string msgString = "服务器异常", ResponseEnum codeEnum=ResponseEnum.ServerError)
        {
            return new MessageModel<T>()
            {
                code = (int)codeEnum,
                success = false,
                msg = msgString,
                data = obj,
            };
        }

        public static MessageModel<T> Success(string msgString="OK")
        {
            return new MessageModel<T>()
            {
                code=(int)ResponseEnum.Success,
                success = true,
                msg = msgString,
            };
        }
        public static MessageModel<T> Success(T obj,string msgString="OK")
        {
            return new MessageModel<T>()
            {
                code = (int)ResponseEnum.Success,
                success = true,
                msg = msgString,
                data=obj,
            };
        }

        public static implicit operator MessageModel<T>(MessageModel<bool> v)
        {
            throw new NotImplementedException();
        }
    }

    
}
