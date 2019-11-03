using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model
{
    public class TableInfoAttribute : Attribute
    {
        /// <summary>
        /// 真实表名(数据库表名，若没有填写默认实体为表名)
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表中文名
        /// </summary>
        public string TableCnName { get; set; }

        /// <summary>
        /// 子表
        /// </summary>
        public Type[] DetailTable { get; set; }

        /// <summary>
        /// 子表中文名
        /// </summary>
        public string DetailTableCnName { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string DBServer { get; set; }


    }
}
