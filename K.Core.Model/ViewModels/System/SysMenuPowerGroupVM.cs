using K.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.ViewModels.System
{
    /// <summary>
    /// 菜单-权限 联系
    /// </summary>
    public class SysMenuPowerGroupVM : BaseExtendTwoEntity
    {
        /// <summary>
        /// 菜单ID          1
        /// </summary>
        public string SysMenuID { get; set; }

        /// <summary>
        /// 菜单权限组ID    多
        /// </summary>
        public string SysPowerGroupID { get; set; }
        
        //public SysMenu SysMenu { get; set; }
        public SysPowerGroup SysPowerGroup { get; set; }
        public List<SysPower> SysPowers { get; set; } = null;
    }
}
