using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model.ViewModels.System
{
    public class SysMenuVM : BaseExtendTwoEntity
    {
        /// <summary>
        ///父级ID
         /// </summary>
        [Display(Name = "父级ID")]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]
        public string ParentId { get; set; }

        /// <summary>
        ///菜单名称
        /// </summary>
        [Display(Name = "菜单名称")]
        [MaxLength(100)]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单链接地址
        /// </summary>
        [Display(Name = "Url")]
        [MaxLength(100)]
        [Editable(true)]
        [Required(AllowEmptyStrings = true)]
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [MaxLength(200)]
        [Editable(true)]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        [MaxLength(100)]
        [Editable(true)]
        public string Icon { get; set; }

        /// <summary>
        ///排序号
        /// </summary>
        [Display(Name = "排序号")]
        [Editable(true)]
        [Required]
        public int? OrderNo { get; set; }


        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示")]
        [Editable(true)]
        [Required]
        public bool? IsShow { get; set; }

        ///// <summary>
        ///// 权限
        ///// </summary>
        //public List<SysPowerGroup> Powers { get; set; }
    }
}
