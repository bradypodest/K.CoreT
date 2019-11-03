using K.Core.IServices.BASE;
using K.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K.Core.IServices
{
    public interface ITopicDetailServices : IBaseServices<TopicDetail>
    {
        Task<List<TopicDetail>> GetTopicDetails();
    }
}
