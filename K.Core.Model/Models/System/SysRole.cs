using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models.System
{
    [TableInfoAttribute(TableName = "SysRole", TableCnName = "角色")]
    public class SysRole : BaseExtendTwoEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        [SugarColumn(Length = 300, IsNullable = true)]
        public string Description { get; set; }

    }
}
