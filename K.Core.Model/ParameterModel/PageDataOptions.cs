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
        /// <summary>
        /// 总数据量
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
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

        public bool Export { get; set; }

        /// <summary>
        /// 主表ID 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 是否查询全部（包含删除的） ，默认不查询
        /// </summary>
        public bool IsAll { get; set; } = false;
    }

    public class SearchParameters
    {
        /// <summary>
        /// 查询字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 显示类型  （可通过这处理为是= , 还是!= 等等）
        /// </summary>
        public string DisplayType { get; set; }
    }
}
