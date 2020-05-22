using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Common.Helper
{
    public sealed class SerialNoHelper
    {
        private static volatile SerialNoHelper helper;
        private static readonly Object syncRoot = new Object();

        private static String lastdate;
        private static Int32 lastno;

        private SerialNoHelper()
        {
        }

        public static SerialNoHelper Helper
        {
            get
            {
                if (helper == null)
                {
                    lock (syncRoot)
                    {
                        if (helper == null)
                            helper = new SerialNoHelper();
                    }
                }
                return helper;
            }
        }

        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="serialno">从数据库读取最大的流水号:流水号格式如下：XX201604120001，2位前缀加8位日期加4位流水号</param>
        /// <returns></returns>
        public String Generate(String serialno)
        {
            lock (syncRoot)
            {
                var today = DateTime.Today.ToString("yyyyMMdd");

                if (today == lastdate)
                    return $"XX{today}{++lastno:0000}";

                lastdate = today;
                lastno = 0;
                if (!String.IsNullOrEmpty(serialno) && serialno.Substring(2, 8) == today)
                    lastno = Convert.ToInt32(serialno.Substring(10));

                return $"XX{today}{++lastno:0000}";
            }
        }
    }
}
