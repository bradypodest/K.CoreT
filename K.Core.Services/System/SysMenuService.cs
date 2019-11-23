using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model.Models;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
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
    }
}
