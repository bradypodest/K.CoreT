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
        [SugarColumn(Length =100,IsNullable = true)]
        public string SysMenuID { get; set; }

        [SugarColumn(Length = 100, IsNullable = true)]
        public string SysPowerGroupID { get; set; }


        [SugarColumn(IsIgnore = true)]
        public SysMenu SysMenu { get; set; }
        [SugarColumn(IsIgnore = true)]
        public SysPowerGroup SysPowerGroup { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<SysPower> SysPowers { get; set; }
    }
}
