using K.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Model.ViewModels
{
    /// <summary>
    /// 保存 实体类
    /// </summary>
    public class SaveModelVM<TEntity>
        where TEntity : BaseExtendTwoEntity  
    {
        /// <summary>
        /// 主表数据
        /// </summary>
        public TEntity MainData { get; set; }
        /// <summary>
        /// 子表数据
        /// </summary>
        [Details]
        public List<object> DetailData { get; set; }

        /// <summary>
        /// 子表删除行的对应ID         以后所有的这个都只能是这个DelKeys 名称，写死了
        /// </summary>
        public List<object> DelKeys { get; set; }

        ///// <summary>
        ///// 从前台传入的其他参数(自定义扩展可以使用)
        ///// </summary>
        //public object Extra { get; set; }
    }
}
