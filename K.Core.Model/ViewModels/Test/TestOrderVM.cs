using K.Core.Common;
using K.Core.Model.Models.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model.ViewModels.Test
{
    public class TestOrderVM : BaseExtendTwoEntity
    {
        //https://blog.csdn.net/WQearl/article/details/82184890  关于DataAnnotations 验证

        /// <summary>
        /// 订单号
        /// </summary>
        [Display(Name = "订单No")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        [Display(Name = "订单数量")]
        [Editable(true)]
        [Required]
        public int Qty { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [Display(Name = "订单备注")]
        [Editable(true)]
        public string Remakes { get; set; }

        [Display(Name = "订单详情")]
        [Details]
        public List<TestOrderDetail> Details { get;set;}


        /// <summary>
        ///  子表删除 ID 集合     以后所有的这个都只能是这个DelKeys 名称，写死了
        /// </summary>
        [Display(Name = "删除订单ID")]
        public List<object> DelKeys { get; set; }
    }
}
