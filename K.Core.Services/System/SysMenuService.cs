using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model.Models;
using K.Core.Model.ViewModels.System;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.Services.System
{
    public class SysMenuService : BaseServices<SysMenu>, ISysMenuService
    {
        ISysMenuRepository _dal;
        IMapper _mapper;
        IUser _httpUser;
        public SysMenuService(ISysMenuRepository dal, IMapper mapper, IUser httpUser)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;
        }

        #region 重写 baseservice方法
        public override async  Task<MessageModel<int>> AddOne(SysMenu saveModel) 
        {
            MessageModel<int> messageModel = MessageModel<int>.Fail();
            //新增前验证
            base.AddOnExecute = async (SysMenu save) =>
            {
                List<SysMenu> sysMenus = await _dal.Query(x => x.Name.Equals(save.Name) && x.ParentId.Equals(save.ParentId) );
                if (sysMenus != null && sysMenus.Count > 0) 
                {
                    return MessageModel<int>.Fail("已经存在该菜单");
                }

                 return MessageModel<int>.Success();
            };

            return  await base.AddOne(saveModel);
        }



        #endregion

        #region ISysMenuService  实现方法
        /// <summary>
        /// 实现 菜单树
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<MessageModel<SysMenuTreeVM>> GetMenuTree(string parentId = "")
        {
            var sysMenus = await _dal.Query(d=>d.Status==Model.StatusE.Live);//里面是没有根节点的,因为根节点是虚拟的

            var sysMenuTrees = (from child in sysMenus
                                       //where child.IsDeleted == false
                                   orderby child.OrderNo
                                   select new SysMenuTreeVM
                                   {
                                       ID = child.ID,
                                       Name = child.Name,
                                       Url=child.Url,
                                       PathUrl=child.PathUrl,
                                       Description=child.Description,
                                       Icon=child.Icon,
                                       OrderNo=child.OrderNo,
                                       IsShow=child.IsShow,
                                       ParentId = child.ParentId,

                                       CreateID=child.CreateID,
                                       CreateTime=child.CreateTime,
                                       Creator=child.Creator,
                                       ModifyID=child.ModifyID,
                                       Modifier=child.Modifier,
                                       ModifyTime=child.ModifyTime,
                                       Status=child.Status
                                   }).ToList();

            //虚拟一个根节点
            SysMenuTreeVM rootRoot = new SysMenuTreeVM
            {
                ID = default(Guid).ToString(),
                ParentId = default(Guid).ToString(),
                Name = "根节点",
                ParentArray = new List<String> {//不知道这块还给不给这个，还是赋值为空，待验证
                    default(Guid).ToString()
                }
            };

            sysMenuTrees = sysMenuTrees.OrderBy(d => d.OrderNo).ToList();
                       
            LoopToAppendChildren(sysMenuTrees, rootRoot, parentId);//不是很懂是如何传递回rootRoot的

            //查询每个节点的父节点 数组
            foreach (var item in sysMenuTrees)
            {
                if (!default(Guid).ToString().Equals(item.ID)) 
                {
                    //
                    List<string> pidArray = new List<string>();

                    //pidArray.Add(item.ID);//不要自己

                    var parent = sysMenuTrees.FirstOrDefault(d => d.ID == item.ParentId);

                    while (parent != null) 
                    {
                        pidArray.Add(parent.ID);
                        parent = sysMenuTrees.FirstOrDefault(d => d.ID == parent.ParentId);
                    }

                    //补上根节点
                    pidArray.Add(default(Guid).ToString());
                    pidArray.Reverse();

                    item.ParentArray = pidArray;

                }
            }


            var messageModel = MessageModel<SysMenuTreeVM>.Success();

            if (messageModel.success)
            {
                messageModel.data = rootRoot;
                messageModel.msg = "获取成功";
            }

            return messageModel;
        }
        #endregion

        /// <summary>
        /// 泛型递归求树形结构
        /// </summary>
        /// <param name="all"></param>
        /// <param name="curItem"></param>
        /// <param name="pid"></param>
        public static void LoopToAppendChildren(List<SysMenuTreeVM> all, SysMenuTreeVM curItem, string pid)
        {

            var subItems = all.Where(ee => ee.ParentId == curItem.ID).ToList();

            if (subItems.Count > 0)
            {
                curItem.children = new List<SysMenuTreeVM>();
                curItem.children.AddRange(subItems);
            }
            else
            {
                curItem.children = null;
            }
            
            foreach (var subItem in subItems)
            {
                if (subItem.ID == pid && ( default(Guid).ToString().Equals(pid)||string.IsNullOrWhiteSpace(pid)))
                {
                    
                }
                LoopToAppendChildren(all, subItem, pid);
            }
        }
    }
}
