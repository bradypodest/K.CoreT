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
    public interface ISysMenuService: IBaseServices<SysMenu>
    {
        /// <summary>
        /// 查询 菜单树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<MessageModel<SysMenuTreeVM>> GetMenuTree(string parentId = "");
    }
}
