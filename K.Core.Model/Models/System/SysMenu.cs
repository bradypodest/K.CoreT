using K.Core.Common.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model.Models
{
    /// <summary>
    /// 
    /// </summary>
    [EntityAttribute(TableCnName = "菜单配置")]
    public class SysMenu : BaseExtendTwoEntity
    {
        /// <summary>
        ///父级ID
        /// </summary>
        [Display(Name = "父级ID")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        [SugarColumn(IsNullable = true)]
        public string ParentId { get; set; }

        /// <summary>
        ///菜单名称
        /// </summary>
        [Display(Name = "菜单名称")]
        [MaxLength(100)]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单链接地址
        /// </summary>
        [Display(Name = "Url")]
        [MaxLength(100)]
        [Editable(true)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Url { get; set; }

        /// <summary>
        /// 菜单页面地址
        /// </summary>
        [Display(Name = "PathUrl")]
        [MaxLength(100)]
        [Editable(true)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string PathUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "Description")]
        [MaxLength(200)]
        [Editable(true)]
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        [MaxLength(100)]
        [Editable(true)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Icon { get; set; }

        /// <summary>
        ///排序号
        /// </summary>
        [Display(Name = "排序号")]
        [Editable(true)]
        public int? OrderNo { get; set; }


        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示")]
        [Editable(true)]
        public bool? IsShow { get; set; }

        ///// <summary>
        ///// 权限
        ///// </summary>
        //[SugarColumn(IsIgnore = true)] //sugar生成表时会忽略该字段
        //public List<SysPowerGroup> Powers { get; set; }
    }
}
