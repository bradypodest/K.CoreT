using K.Core.Common.Helper.AutofacManager;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model;
using K.Core.Model.Models;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using K.Core.Extensions;
using K.Core.Common.Model;
using AutoMapper;
using K.Core.Common.HttpContextUser;

namespace K.Core.Services.System
{
    public class SysUserService : BaseServices<SysUser>, ISysUserService
    {
        ISysUserRepository _dal;
        IMapper _mapper;
        IUser _httpUser;
        public SysUserService(ISysUserRepository dal,IMapper mapper, IUser httpUser)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;
            _mapper = mapper;
        }

        /// <summary>
        /// 这个是为了在service 中使用本service 的
        /// 比如：在SysRoleService 中某方法要用到SysUserService 实例 则，可以SysUserService.Instance(); 获取到
        /// </summary>
        public static ISysUserService Instance
        {
            get { return AutofacContainerModule.GetService<ISysUserService>(); }
        }

        #region 重写BaseService 中方法
        //public override async Task<MessageModel<int>> AddOne(SysUserVM tvm)
        //{
        //    MessageModel<int> resp = new MessageModel<int>();

        //    MessageModel<SysUserVM> valiresp = new MessageModel<SysUserVM>();
        //    valiresp = tvm.ValidationEntity();
        //    if (!valiresp.success) 
        //    {
        //        resp.success = false;
        //        resp.msg = valiresp.msg;
        //        resp.data = 0;

        //        return resp;
        //    }

        //    // 注意就是这里,mapper
        //    SysUser addSysUser  = _mapper.Map<SysUser>(tvm);

        //    addSysUser.CreateID = _httpUser.ID ?? "";
        //    addSysUser.CreateTime = DateTime.Now;
        //    addSysUser.Creator = _httpUser.Name ?? "";


        //    resp.data = await baseDal.Add(addSysUser);

        //    resp.success = true;
        //    resp.msg = "OK";
        //    resp.data = 1;
        //    return resp;
        //}

        #endregion

        #region ISysUserService 实现方法


        #endregion
    }
}
