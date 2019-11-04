using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace K.Core.Model
{
    public class BaseExtendEntity : BaseEntity
    {
        //1.ColumnName 定义数据库表字段的真实名称，当一样的时候可以不定义该特性

        //2.IsIgnore 查询 增加 删除 更新会过滤这一列，如果想让查询也有这一列可以用.Select("*")强制查所有列

        //3.IsPrimaryKey 标识是否为主键，更新和插入的时候会根据主键值判段更新哪条，当InitKey为Attribute时一定要加该特性不然找不到主键，Systemtable加了没有效果

        //4.IsIdentity 是否为自增长  ，更新和插入的时候会根据会有用，当InitKey为Attribute时一定要加该特性，Systemtable加了没有效果

        //5.ColumnDescription 列描述，暂时未实现该功能

        //6.Length 长度，生成表会用到

        //7.IsNullable 是否可空，生成表会用到

        //8.OldColumnName 修改列名，生成表会用到

        //9.ColumnDataType 自定义生成的数据类型，生成表会用到

        /// <summary>
        /// ID
        /// </summary>
        [Key]//主键
        [Display(Name = "序号")]//字段显示名称
        //[Column(TypeName = "nvarchar(100)")] //EF
        //[Required(AllowEmptyStrings = false)]//必需
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, Length =100)]
        public string ID { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        //[Column(TypeName = "int")]
        [Editable(true)]
        [SugarColumn(IsNullable = false)]
        public StatusE Status { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public System.DateTime? DeleteTime { get; set; }

        /// <summary>
        ///删除人
        /// </summary>
        [Display(Name = "删除人")]
        [MaxLength(200)]
        //[Column(TypeName = "nvarchar(200)")]
        [SugarColumn(IsNullable = true, Length = 200)]
        [Editable(true)]
        public string Deleter { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        [Display(Name = "删除人ID")]
        //[Column(TypeName = "nvarchar(100)")]
        [SugarColumn(IsNullable = true, Length = 100)]
        public String DeleterID { get; set; }
    }

    public enum StatusE
    {
        /// <summary>
        /// 存活
        /// </summary>
        Live = 0,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 禁止  ，如用户
        /// </summary>
        Ban = 2,
    }
}
