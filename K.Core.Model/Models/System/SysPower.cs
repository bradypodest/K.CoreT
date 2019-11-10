using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models.System
{
    /// <summary>
    /// 单个权限 （一个api 接口）
    /// </summary>
    public class SysPower: BaseExtendTwoEntity
    {

        /// <summary>
        /// 权限组ID
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string SysPowerGroupID { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 权限api
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string ApiUrl { get; set; }

        /// <summary>
        /// 是否公开api   : 0 否，1 true
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsOpen { get; set; }



    }
}
