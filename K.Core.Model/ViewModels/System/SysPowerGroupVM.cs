using K.Core.Model.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.ViewModels.System
{
    /// <summary>
    /// 权限组（一组api接口组成的一个权限） 比如 查询权限里面包含那些接口
    /// </summary>
    public class SysPowerGroupVM : BaseExtendTwoEntity
    {
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 包含的权限api
        /// </summary>
        public List<SysPower> SysPowers { get; set; }
    }
}
