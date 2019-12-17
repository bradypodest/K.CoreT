using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models
{
    /// <summary>
    /// 菜单-权限组
    /// </summary>
    public class SysMenuPowerGroup:BaseExtendTwoEntity
    {
        /// <summary>
        /// 菜单ID          1
        /// </summary>
        [SugarColumn(Length =100,IsNullable = true)]
        public string SysMenuID { get; set; }

        /// <summary>
        /// 菜单权限组ID    多
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string SysPowerGroupID { get; set; }


        //[SugarColumn(IsIgnore = true)]
        //public SysMenu SysMenu { get; set; }
        //[SugarColumn(IsIgnore = true)]
        //public SysPowerGroup SysPowerGroup { get; set; }
        //[SugarColumn(IsIgnore = true)]
        //public List<SysPower> SysPowers { get; set; }
    }
}
