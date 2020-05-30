using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models.Test
{
    /// <summary>
    /// 本类是一个测试订单 表 ，用于测试功能时候
    /// </summary>
    [TableInfoAttribute(TableCnName = "订单", TableName = "TestOrder", DetailTable = new Type[] { typeof(TestOrderDetail) }, DetailTableCnName = "订单明细")]
    public class TestOrder:BaseExtendTwoEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Qty { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [SugarColumn(Length = 300, IsNullable = true)]
        public string Remakes { get; set; }
    }
}
