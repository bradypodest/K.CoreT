﻿using AutoMapper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IRepository.Test;
using K.Core.IServices.Test;
using K.Core.Model.Models.Test;
using K.Core.Services.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.Services.Test
{
    public class TestOrderService : BaseServices<TestOrder>, ITestOrderService
    {
        ITestOrderRepository _dal;
        IMapper _mapper;
        IUser _httpUser;

        ITestOrderRepository _testOrderRepository;

        public TestOrderService(ITestOrderRepository dal, IMapper mapper, IUser httpUser)
        {
            this._dal = dal;
            base.baseDal = dal;

            _httpUser = httpUser;
            base._httpUser = httpUser;

            _mapper = mapper;
            base._mapper = mapper;
        }

        #region 重写 baseservice方法
        public override async Task<MessageModel<bool>> AddOne(TestOrder saveModel)
        {
            MessageModel<int> messageModel = MessageModel<int>.Fail();

            //新增前验证
            base.AddOnExecute = async (TestOrder save) =>
            {
                List<TestOrder> testOrders = await _dal.Query(x => x.OrderNo.Equals(save.OrderNo));
                if (testOrders != null && testOrders.Count > 0)
                {
                    return MessageModel<bool>.Fail(false,"已经存在该订单");
                }

                return MessageModel<bool>.Success(true);
            };

            return await base.AddOne(saveModel);
        }

        public override async Task<MessageModel<bool>> DeleteOne(string ID)
        {
            MessageModel<bool> messageModel = MessageModel<bool>.Fail();

            //删除前验证
            //base.DelOnExecute = async (TestOrder modelExecute) =>
            //{
            //    //查询是否有该角色
            //    List<TestOrder> testOrders = await _dal.Query(x => x.ID.Equals(modelExecute.ID) && x.Status == Model.StatusE.Live);

            //    if (testOrders != null && testOrders.Count > 0)
            //    {

            //    }
            //    else
            //    {
            //        return MessageModel<bool>.Fail("不存在该订单");
            //    }

            //    ////查询是否有用户有该对应的角色
            //    //List<TestOrder> sysUsers = await _sysUserRepository.Query(x => x.Status == Model.StatusE.Live && x.RoleId == modelExecute.RoleID);

            //    //if (sysUsers != null && sysUsers.Count > 0)
            //    //{
            //    //    return MessageModel<bool>.Fail("有用户是该角色");
            //    //}

            //    return MessageModel<bool>.Success();
            //};

            return await base.DeleteOne(ID);


        }
        #endregion

    }
}
