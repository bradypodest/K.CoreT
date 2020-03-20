using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.ViewModels.System
{
    /// <summary>
    /// 角色-菜单-权限组  联系
    /// </summary>
    public class SysRoleMenuPowerGVM : BaseExtendTwoEntity
    {
        /// <summary>
        /// 角色ID    1
        /// </summary>
        public string RoleID { get; set; }

        /// <summary>
        /// 菜单ID   多
        /// </summary>
        public string MenuID { get; set; }

        /// <summary>
        /// 权限组ID   多
        /// </summary>
        public string PowerGroupID { get; set; }


    }
}
