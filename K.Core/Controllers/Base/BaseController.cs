using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using K.Core.AutoMapper;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using K.Core.Extensions;
using K.Core.IServices.BASE;
using K.Core.Model;
using K.Core.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        protected IMapper _mapper;
        protected IUser _httpUser;

        public BaseController()
        {

        }

        public BaseController(IEntityService service,IUser httpUser,IMapper mapper)
        {
            _service = service;
            _httpUser = httpUser;
            _mapper = mapper;
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
        //[Authorize("admin")]//实验jwt 验证 ：角色名称为 “admin”
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
        public virtual async Task<MessageModel<int>> Add([FromBody]TVM tvm)
        {
            //验证信息放在拦截器中

            //将数据转化为T
            var source = new Source<TVM> { Value = tvm };
            var t = _mapper.Map<Destination<T>>(source);
            return await _service.AddOne(t.Value);

            //var t = _mapper.Map<SysUser>(tvm);

            //return await _service.AddOne(t);
        }

        /// <summary>
        /// base 更新一个实体 （需要在各自实体control重写,实体类service中写最新的方法）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public virtual async Task<MessageModel<bool>> Update([FromBody]TVM tvm)
        {
            //将数据转化为T
            var source = new Source<TVM> { Value = tvm };
            var t = _mapper.Map<Destination<T>>(source);

            return await _service.UpdateOne(t.Value);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageDataOptions"></param>
        /// <returns></returns>
        [HttpPost, Route("GetDetailPageData")]
        public virtual async Task<MessageModel<object>> GetDetailPageData([FromBody]PageDataOptions pageDataOptions) 
        {
            //return await _service.GetDetailPageData(pageDataOptions);

            object pageData = await Task.FromResult(_service.GetDetailPageData(pageDataOptions));

            return MessageModel<object>.Success(pageData, "OK");

        }
    }
}