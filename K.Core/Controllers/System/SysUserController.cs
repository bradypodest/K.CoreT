using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.AuthHelper.OverWrite;
using K.Core.AutoMapper;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.Controllers.Base;
using K.Core.IServices.System;
using K.Core.Model;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.System
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/SysUser")]
    public class SysUserController : BaseController<SysUser,SysUserVM,ISysUserService>
    {
        readonly ISysUserService _sysUserServices;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public SysUserController(ISysUserService service,IUser httpUser,IMapper mapper)
        : base( service, httpUser,mapper)
        {
            _sysUserServices = service;
            _user = httpUser;
            _mapper = mapper;
        }

        #region  重写baseController 方法


        #endregion

        #region  其他方法

        /// <summary>
        /// 获取用户详情根据token
        /// 【无权限】
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpGet, Route("GetInfoByToken")]
        [AllowAnonymous]
        public async Task<MessageModel<SysUserVM>> GetInfoByToken(string token)
        {
            var data = MessageModel<SysUserVM>.Fail();

            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                //if (tokenModel != null && tokenModel.Uid > 0)
                if (tokenModel != null && !string.IsNullOrWhiteSpace(tokenModel.Uid))
                {
                    var sysUserInfo = await _sysUserServices.QueryById(tokenModel.Uid);
                    if (sysUserInfo != null)
                    {
                        //转换
                        //将数据转化为T
                        var source = new Source<SysUser> { Value = sysUserInfo };
                        var t = _mapper.Map<Destination<SysUserVM>>(source);

                        data = MessageModel<SysUserVM>.Success(t.Value);

                        return data;
                    }
                }

            }

            return data;
        }


        #endregion



    }
}