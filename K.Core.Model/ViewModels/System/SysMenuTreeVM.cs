using K.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.ViewModels.System
{
    public class SysMenuTreeVM : BaseExtendTwoEntity
    {
        /// <summary>
        ///父级ID
         /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        ///菜单Id （字母名称）,如：SysUser
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        ///菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 菜单页面地址
        /// </summary>
        public string PathUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///排序号
        /// </summary>
        public int? OrderNo { get; set; }


        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<SysMenuTreeVM> Children { get; set; } = null;

        /// <summary>
        /// 父节点数组（追溯到根节点）
        /// </summary>
        public List<string> ParentArray { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<SysPowerGroupVM> PowerGroups { get; set; }
    }
}
