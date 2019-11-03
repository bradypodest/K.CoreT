using K.Core.Common.Helper.AutofacManager;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model.Models;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Services.System
{
    public class SysUserService : BaseServices<SysUser>, ISysUserService
    {
        ISysUserRepository _dal;
        public SysUserService(ISysUserRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 这个是为了在service 中使用本service 的
        /// 比如：在SysRoleService 中某方法要用到SysUserService 实例 则，可以SysUserService.Instance(); 获取到
        /// </summary>
        public static ISysUserService Instance
        {
            get { return AutofacContainerModule.GetService<ISysUserService>(); }
        }
    }
}
