using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace K.Core.Common.Helper
{
    /// <summary>
    /// ModelState扩展类
    /// </summary>
    public static class ModelStateExtension
    {
        /// <summary>
        /// 获取模型绑定中的第一条错误信息
        /// </summary>
        /// <param name="msDictionary"></param>
        /// <returns></returns>
        public static string GetFirstErrMsg(this ModelStateDictionary msDictionary)
        {
            if (msDictionary.IsValid || !msDictionary.Any()) return "";
            foreach (string key in msDictionary.Keys)
            {
                ModelStateEntry tempModelState = msDictionary[key];
                if (tempModelState.Errors.Any())
                {
                    var firstOrDefault = tempModelState.Errors.FirstOrDefault();
                    if (firstOrDefault != null)
                        return firstOrDefault.ErrorMessage;
                }
            }
            return "";
        }
        /// <summary>
        ///  获取错误信息列表
        /// </summary>
        /// <param name="msDictionary"></param>
        /// <returns></returns>
        public static List<string> GetErrMsgList(this ModelStateDictionary msDictionary)
        {
            var list = new List<string>();
            if (msDictionary.IsValid || !msDictionary.Any()) return list;

            //获取所有错误的Key
            foreach (string key in msDictionary.Keys)
            {
                ModelStateEntry tempModelState = msDictionary[key];
                if (tempModelState.Errors.Any())
                {
                    var errorList = tempModelState.Errors.ToList();
                    foreach (var item in errorList)
                    {
                        list.Add(item.ErrorMessage);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取ModelState所有错误信息，间隔符间隔
        /// </summary>
        /// <param name="splitStr">间隔符</param>
        /// <returns></returns>
        public static string GetAllErrMsgStr(this ModelStateDictionary msDictionary, string splitStr)
        {
            var returnStr = "";
            if (msDictionary.IsValid || !msDictionary.Any()) return returnStr;

            //获取所有错误的Key
            foreach (string key in msDictionary.Keys)
            {
                ModelStateEntry tempModelState = msDictionary[key];
                if (tempModelState.Errors.Any())
                {
                    var errorList = tempModelState.Errors.ToList();
                    foreach (var item in errorList)
                    {
                        returnStr += item.ErrorMessage + splitStr;
                    }
                }
            }
            return returnStr;
        }
    }
}
