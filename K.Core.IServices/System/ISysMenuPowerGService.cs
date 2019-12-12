using K.Core.Common.Model;
using K.Core.IServices.BASE;
using K.Core.Model.Models;
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
        Task<MessageModel<List<SysMenuPowerGroup>>> GetMenuPowerGroups(string menuId);
    }
}
