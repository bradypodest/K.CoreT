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
    public interface ISysMenuPowerGService: IBaseServices<SysMenuPowerGroup>
    {
        /// <summary>
        /// 查询到对应的菜单的菜单权限组与菜单权限
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<MessageModel<List<SysMenuPowerGroupVM>>> GetMenuPowerGroups(string menuId);

        /// <summary>
        /// 更新菜单对应的菜单权限
        /// </summary>
        /// <param name="sysMenuPowerG"></param>
        /// <returns></returns>
        Task<MessageModel<bool>> UpdateMenuPowerGroups(List<SysMenuPowerGroupVM> sysMenuPowerG);
    }
}
