using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model
{
    public class PageDataOptions
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 多少 pagesize
        /// </summary>
        public int Rows { get; set; } = 50;
        public int Total { get; set; }
        public string TableName { get; set; }
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式    格式如  "ID desc"
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 条件 格式：   字段名称,条件（如 0,1,2）,值 | 字段名称,条件（如）,值
        /// 关于条件 ，查看ExpressionType 枚举
        /// </summary>
        public string Wheres { get; set; }

        public string Foots { get; set; }
        public bool Export { get; set; }

        public object Value { get; set; }

        public bool IsAll { get; set; } = false;
    }
}
