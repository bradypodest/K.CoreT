﻿using System;
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


        public static MessageModel<T> Fail(string msgString="服务器异常") {
            return new MessageModel<T>()
            {
                success = false,
                msg = msgString,
                data = default(T),
            };
        }

        public static MessageModel<T> Fail(T obj ,string msgString= "服务器异常")
        {
            return new MessageModel<T>()
            {
                success = false,
                msg = msgString,
                data = obj,
            };
        }
        
        public static MessageModel<T> Success(string msgString="OK")
        {
            return new MessageModel<T>()
            {
                success = true,
                msg = msgString,
            };
        }
        public static MessageModel<T> Success(T obj,string msgString="OK")
        {
            return new MessageModel<T>()
            {
                success = true,
                msg = msgString,
                data=obj,
            };
        }
    }
}