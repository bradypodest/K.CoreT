using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K.Core.IRepository;
using K.Core.Model.Models;
using K.Core.Repository.Base;

namespace K.Core.Repository
{
    public class TopicRepository: BaseRepository<Topic>, ITopicRepository
    {
    }
}
