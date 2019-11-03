using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K.Core.Common;
using K.Core.IRepository;
using K.Core.IServices;
using K.Core.Model.Models;
using K.Core.Services.BASE;

namespace K.Core.Services
{
    public class TopicDetailServices : BaseServices<TopicDetail>, ITopicDetailServices
    {
        ITopicDetailRepository _dal;
        public TopicDetailServices(ITopicDetailRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取开Bug数据（缓存）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<TopicDetail>> GetTopicDetails()
        {
            return await base.Query(a => !a.tdIsDelete && a.tdSectendDetail == "tbug");
        }
    }
}
