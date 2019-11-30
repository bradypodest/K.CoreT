using K.Core.Common.Helper.AutofacManager;
using K.Core.IRepository.System;
using K.Core.Model.Models;
using K.Core.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Repository.sugar.System
{
    /// <summary>
    /// 系统用户 仓库
    /// </summary>
    public class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
    {
        public static ISysUserRepository Instance
        {
            get { return AutofacContainerModule.GetService<ISysUserRepository>(); }
        }

    }
}
