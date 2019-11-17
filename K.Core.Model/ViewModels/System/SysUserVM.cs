using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model
{
    public class SysUserVM : BaseExtendTwoEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [MaxLength(100)]
        [Editable(true)]
        [Required( ErrorMessage = "请输入账号",AllowEmptyStrings = false)]
        public String UserName { get; set; }

        /// <summary>
        ///密码
        /// </summary>
        [Display(Name = "密码")]
        [MaxLength(20)]
        [Required(ErrorMessage = "请输入密码", AllowEmptyStrings = false)]
        //[JsonIgnore]//忽略其属性序列号 //可以在swagger 上面不显示
        public string UserPwd { get; set; }

        /// <summary>
        ///角色id
        /// </summary>
        [MaxLength(100)]
        //[Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public String RoleId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        [MaxLength(150)]
        [Editable(true)]
        public string RoleName { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        [Display(Name = "错误次数")]
        //[Column(TypeName = "int")]
        public int ErrorCount { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        public string HeadPicUrl { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Display(Name = "年龄")]
        public int? Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        public DateTime? Birth { get; set; } = DateTime.Now;

        /// <summary>
        ///地址
        /// </summary>
        [Display(Name = "地址")]
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        ///电话
        /// </summary>
        [Display(Name = "电话")]
        [MaxLength(100)]
        [Editable(true)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [MaxLength(100)]
        [Editable(true)]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(400)]
        [Editable(true)]
        public string Remark { get; set; }
    }
}
