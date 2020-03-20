using K.Core.Common.Model;
using K.Core.IServices.BASE;
using K.Core.Model.Models;
using K.Core.Model.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.IServices.System
{
    public interface ISysRoleMenuPowerGService : IBaseServices<SysRoleMenuPowerGroup>
    {
        /// <summary>
        /// 更新角色对应菜单，对应权限组
        /// </summary>
        /// <param name="sysRoleMenuPowerGVMs"></param>
        /// <returns></returns>
        Task<MessageModel<bool>> UpdateRoleMenuPowerGs(List<SysRoleMenuPowerGVM> sysRoleMenuPowerGVMs);
    }
}
