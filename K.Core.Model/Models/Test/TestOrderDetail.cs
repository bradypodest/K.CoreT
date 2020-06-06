using K.Core.Common.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace K.Core.Model.Models.Test
{
    /// <summary>
    /// 订单详情
    /// </summary>
    [EntityAttribute(TableName = "TestOrderDetail", TableCnName = "订单详情")]
    public class TestOrderDetail: BaseExtendTwoEntity
    {
        /// <summary>
        /// 订单ID  以后所有外键的值都只能起 RefID 这个名字了 
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        [Required]
        [Display(Name = "订单ID")]
        public string RefID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        [Required]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "商品名称")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品批次
        /// </summary>
        [SugarColumn(Length = 50)]
        [Required]
        [Display(Name = "商品批次")]
        public string GoodsBatch { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [SugarColumn(IsNullable = false)]
        [Required]
        [Display(Name = "数量")]
        public int Qty { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
       [Required]
       [Display(Name = "重量")]
        public double Weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 300, IsNullable = true)]
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
