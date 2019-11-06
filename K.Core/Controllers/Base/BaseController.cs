using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.IServices.BASE;
using K.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.Base
{
    public class BaseController <T,TVM,IEntityService>: Controller
        where T : BaseExtendTwoEntity
        where TVM: BaseExtendTwoEntity
        where IEntityService : IBaseServices<T>
    {
        /// <summary>
        /// 这个是对应实体类的 service
        /// </summary>
        protected IEntityService _service;

        protected IUser _httpUser;

        public BaseController()
        {

        }

        public BaseController(IEntityService service,IUser httpUser)
        {
            _service = service;
            _httpUser = httpUser;
        }

        /// <summary>
        /// base 分页获取
        /// </summary>
        /// <param name="pageDataOptions">分页参数</param>
        /// <returns></returns>
        [HttpPost, Route("GetPageData")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<PageModel<T>>> GetPageData([FromBody]PageDataOptions pageDataOptions)
        {
            return await _service.GetPageData(pageDataOptions);
        }

        /// <summary>
        /// base 根据id获取单独的一个实体  （看情况是否需要重写）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("admin")]//实验jwt 验证 ：角色名称为 “admin”
        [HttpGet,Route("GetOneByID")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<T>> GetOneByID(string id) 
        {
            return await _service.GetOneByID(id);
        }

        /// <summary>
        /// base 增加一个实体 （看情况是否重写）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost, Route("Add")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<int>> Add([FromBody]T t)
        {

            //_ = ModelState.IsValid;

            return await _service.AddOne(t);
        }

        /// <summary>
        /// base 更新一个实体 （需要在各自实体control重写,实体类service中写最新的方法）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public virtual async Task<MessageModel<bool>> Update([FromBody]T t)
        {
            return await _service.UpdateOne(t);
        }
        
        /// <summary>
        /// base 根据id 删除一个实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("Delete")]
        public virtual async Task<MessageModel<bool>> Delete(string id)
        {
            return await _service.DeleteOne(id);
        }
    }
}