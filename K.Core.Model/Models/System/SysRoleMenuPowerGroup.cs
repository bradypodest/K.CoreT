using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.Models.System
{
    /// <summary>
    /// 角色-菜单-权限组
    /// </summary>
    public class SysRoleMenuPowerGroup
    {
        [SugarColumn(Length =100,IsNullable =false)]
        public string RoleID { get; set; }

        [SugarColumn(Length = 100, IsNullable = false)]
        public string MenuID { get; set; }

        [SugarColumn(Length = 100, IsNullable = false)]
        public string PowerGroupID { get; set; }

        // 下边三个实体参数，只是做传参作用，所以忽略下
        [SugarColumn(IsIgnore = true)]
        public SysRole SysRole { get; set; }
        [SugarColumn(IsIgnore = true)]
        public SysMenu SysMenu { get; set; }
        [SugarColumn(IsIgnore = true)]
        public SysPowerGroup SysPowerGroup { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<SysPower> SysPowers { get; set; }
    }
}
