using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model
{
    public class PageDataOptions
    {
        /// <summary>
        /// 当前页
        /// </summary>
        [Display(Name = "当前页")]
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 多少 pagesize
        /// </summary>
        [Display(Name = "页大小")]
        public int PageSize { get; set; } = 50;
        public int Total { get; set; }
        public string TableName { get; set; }
        /// <summary>
        /// 排序方式    格式如  "ID desc" 默认为 CreateTime desc
        /// </summary>
        public string Order { get; set; } = "CreateTime desc";
        /// <summary>
        /// 条件 格式：   字段名称,条件（如 0,1,2）,值 | 字段名称,条件（如）,值
        /// 关于条件 ，查看ExpressionType 枚举
        /// </summary>
        public string Wheres { get; set; }

        public string Foots { get; set; }
        public bool Export { get; set; }

        public object Value { get; set; }

        /// <summary>
        /// 是否查询全部（包含删除的） ，默认不查询
        /// </summary>
        public bool IsAll { get; set; } = false;
    }
}
