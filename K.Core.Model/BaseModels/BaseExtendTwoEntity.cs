using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model
{
    public class BaseExtendTwoEntity : BaseExtendEntity
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "CreateID")]
        //[Column(TypeName = "nvarchar(100)")]
        [SugarColumn(IsNullable = false, Length = 100)]
        public string CreateID { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Display(Name = "创建人")]
        [MaxLength(200)]
        //[Column(TypeName = "nvarchar(200)")]
        [SugarColumn(IsNullable = false,  Length = 200)]
        [Editable(true)]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; }


        /// <summary>
        /// 修改人ID
        /// </summary>
        [Display(Name = "ModifyID")]
        //[Column(TypeName = "nvarchar(100)")]
        [SugarColumn(IsNullable = true, Length = 100)]
        public String ModifyID { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        [Display(Name = "修改人")]
        [MaxLength(200)]
        [SugarColumn(IsNullable = true, Length = 100)]
        //[Column(TypeName = "nvarchar(200)")]
        public string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? ModifyTime { get; set; }

    }

    
}
