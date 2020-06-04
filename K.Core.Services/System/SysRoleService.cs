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
    public class SysRoleService : BaseServices<SysRole>, ISysRoleService
    {
        ISysRoleRepository _dal;
        IMapper _mapper;
        IUser _httpUser;

        ISysUserRepository _sysUserRepository;

        public SysRoleService(ISysRoleRepository dal, IMapper mapper, IUser httpUser, ISysUserRepository sysUserRepository)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;

            _sysUserRepository = sysUserRepository;
        }

        #region 重写 baseservice方法
        public override async Task<MessageModel<bool>> AddOne(SysRole saveModel)
        {
            MessageModel<int> messageModel = MessageModel<int>.Fail();

            //新增前验证
            base.AddOnExecute = async (SysRole save) =>
            {
                List<SysRole> sysRoles = await _dal.Query(x => x.RoleID.Equals(save.RoleID));
                if (sysRoles != null && sysRoles.Count > 0)
                {
                    return MessageModel<bool>.Fail(false,"已经存在该角色");
                }

                return MessageModel<bool>.Success(true);
            };

            return await base.AddOne(saveModel);
        }

        public override async Task<MessageModel<bool>> DeleteOne(string ID)
        {
            MessageModel<bool> messageModel = MessageModel<bool>.Fail();

            //删除前验证
            base.DelOnExecute = async (SysRole modelExecute) =>
            {
                //查询是否有该角色
                List<SysRole> sysRoles = await _dal.Query(x => x.RoleID.Equals(modelExecute.RoleID)&& x.Status == Model.StatusE.Live);

                if (sysRoles != null && sysRoles.Count > 0)
                {

                }
                else
                {
                    return MessageModel<bool>.Fail("不存在该角色");
                }

                //查询是否有用户有该对应的角色
                List<SysUser> sysUsers = await _sysUserRepository.Query(x => x.Status == Model.StatusE.Live && x.RoleId == modelExecute.RoleID);

                if (sysUsers != null && sysUsers.Count > 0) 
                {
                    return MessageModel<bool>.Fail("有用户是该角色");
                }

                return MessageModel<bool>.Success();
            };

            return await base.DeleteOne(ID);


        }
        #endregion
    }
}
