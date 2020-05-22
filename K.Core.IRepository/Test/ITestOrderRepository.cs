using K.Core.IRepository.Base;
using K.Core.Model.Models.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.IRepository.Test
{
    /// <summary>
    /// 订单测试
    /// </summary>
    public interface ITestOrderRepository : IBaseRepository<TestOrder>
    {
    }
}
