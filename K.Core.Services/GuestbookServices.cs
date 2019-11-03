using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K.Core.IRepository;
using K.Core.IServices;
using K.Core.Model.Models;
using K.Core.Services.BASE;

namespace K.Core.Services
{
    public class GuestbookServices : BaseServices<Guestbook>, IGuestbookServices
    {
        IGuestbookRepository _dal;
        public GuestbookServices(IGuestbookRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
