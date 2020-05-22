using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models.Test
{
    /// <summary>
    /// 订单详情
    /// </summary>
    [TableInfoAttribute(TableName = "TestOrderDetail", TableCnName = "订单详情")]
    public class TestOrderDetail: BaseExtendTwoEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string OrderID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品批次
        /// </summary>
        [SugarColumn(Length = 50)]
        public string GoodsBatch { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Qty { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 300, IsNullable = true)]
        public string Remakes { get; set; }
    }
}
