using K.Core.Common.Model;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model.Models
{
    [EntityAttribute(TableName = "SysUser", TableCnName = "系统用户")]
    public class SysUser : BaseExtendTwoEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        //[Display(Name = "用户名")]
        //[MaxLength(100)]
        //[Column(TypeName = "navarchar(100)")]
        [SugarColumn(Length = 100, IsNullable = false)]
        //[Editable(true)]
        //[Required(AllowEmptyStrings = false)]
        public String UserName { get; set; }

        /// <summary>
        ///密码
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = false)]
        //[Column(TypeName = "nvarchar(200)")]
        public string UserPwd { get; set; }

        /// <summary>
        ///角色id
        /// </summary>
        //[Display(Name = "角色id")]
        //[MaxLength(600)]
        //[Column(TypeName = "nvarchar(100)")]
        //[Editable(true)]
        //[Required(AllowEmptyStrings = false)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public String RoleId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        [MaxLength(150)]
        //[Column(TypeName = "nvarchar(150)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        [SugarColumn(Length = 150, IsNullable = true)]
        public string RoleName { get; set; }

        /// <summary>
        ///最后登陆时间
        /// </summary>
        [Display(Name = "最后登陆时间")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        ///最后密码修改时间
        /// </summary>
        [Display(Name = "最后密码修改时间")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "datetime")]
        public DateTime? LastModifyPwdDate { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        [Display(Name = "错误次数")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "int")]
        public int ErrorCount { get; set; }

        /// <summary>
        ///最后登陆失败时间
        /// </summary>
        [Display(Name = "最后登陆失败时间")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "datetime")]
        public DateTime? LastLoginFailDate { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        [SugarColumn(Length = 200, IsNullable = true)]
        public string HeadPicUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        //[Column(TypeName = "nvarchar(100)")]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Display(Name = "年龄")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "int")]
        public int? Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        [SugarColumn(IsNullable = true)]
        //[Column(TypeName = "datetime")]
        public DateTime? Birth { get; set; } = DateTime.Now;

        /// <summary>
        ///地址
        /// </summary>
        [Display(Name = "地址")]
        [MaxLength(300)]
        //[Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        [SugarColumn(Length = 300, IsNullable = true)]
        public string Address { get; set; }

        /// <summary>
        ///电话
        /// </summary>
        [Display(Name = "电话")]
        [MaxLength(100)]
        //[Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [MaxLength(100)]
        //[Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(400)]
        //[Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        [SugarColumn(Length = 400, IsNullable = true)]
        public string Remark { get; set; }
    }
}
