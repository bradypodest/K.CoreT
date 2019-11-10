using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.IRepository.System;
using K.Core.IServices.System;
using K.Core.Model.Models;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Services.System
{
    class SysRoleMenuPowerGService : BaseServices<SysRoleMenuPowerGroup>, ISysRoleMenuPowerGService
    {
        ISysRoleMenuPowerGRepository _dal;
        IMapper _mapper;
        IUser _httpUser;
        public SysRoleMenuPowerGService(ISysRoleMenuPowerGRepository dal, IMapper mapper, IUser httpUser)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;
        }

    }
}
