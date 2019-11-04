using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.IServices.BASE;
using K.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace K.Core.Controllers.Base
{
    public class BaseController <T,IEntityService>: Controller
        where T : BaseExtendTwoEntity
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
        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("GetPageData")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<PageModel<T>>> GetPageData([FromBody]PageDataOptions pageDataOptions)
        {
            MessageModel<PageModel<T>> resp = new MessageModel<PageModel<T>>();

            //检查传入参数是否有误 （未实现） 

            var oLamadaExtention = LambdaHelper.True<T>();

            resp.Data= await _service.QueryPage(oLamadaExtention, pageDataOptions);

            resp.Success = true;
            resp.Msg = "OK";

            return resp;
        }

        /// <summary>
        /// base 根据id获取单独的一个实体  （看情况是否需要重写）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Route("GetOneByID")]
        [ResponseCache(Duration = 60)]
        public virtual async Task<MessageModel<T>> GetOneByID(string id) 
        {
            MessageModel<T> resp = new MessageModel<T>();

            //传入参数检查


            resp.Data = await _service.QueryById(id);

            resp.Success = true;
            resp.Msg = "OK";
            return resp;
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
            MessageModel<int> resp = new MessageModel<int>();

            //传入参数检查

            //t.CreateID = _httpUser.ID??"admin";
            //t.CreateTime = DateTime.Now;
            //t.Creator = _httpUser.Name ?? "admin";

            //jwt 还没有，使用下面
            t.ID = Guid.NewGuid().ToString();
            t.CreateID =  "admin";
            t.CreateTime = DateTime.Now;
            t.Creator =  "admin";


            resp.Data = await _service.Add(t);

            resp.Success = true;
            resp.Msg = "OK";
            return resp;
        }

        /// <summary>
        /// base 更新一个实体 （需要在各自实体control中重写）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost, Route("Update")]
        public virtual async Task<MessageModel<bool>> Update([FromBody]T t)
        {
            MessageModel<bool> resp = new MessageModel<bool>();

            //传入参数检查

            //t.Modifier = _httpUser.Name;
            //t.ModifyID = _httpUser.ID;
            //t.ModifyTime = DateTime.Now;

            t.Modifier = "adminU";
            t.ModifyID = "adminU";
            t.ModifyTime = DateTime.Now;

            //List<string> lstColumns = new List<string>();
            //lstColumns.Add("CreateTime");

            List<string> lstIgnoreColumns = new List<string>();
            lstIgnoreColumns.Add("ID");
            lstIgnoreColumns.Add("Status");
            lstIgnoreColumns.Add("Creator");
            lstIgnoreColumns.Add("CreateID");
            lstIgnoreColumns.Add("CreateTime");

            resp.Data = await _service.Update(t,null,lstIgnoreColumns,"ID="+t.ID);

            resp.Success = true;
            resp.Msg = "OK";
            return resp;
        }
        
        /// <summary>
        /// base 根据id 删除一个实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("Delete")]
        public virtual async Task<MessageModel<bool>> Delete(string ID)
        {
            MessageModel<bool> resp = new MessageModel<bool>();

            //传入参数检查

            T deleteOne = await _service.QueryById(ID);

            //标志删除
            deleteOne.Status = StatusE.Delete;
            //deleteOne.DeleterID = _httpUser.ID;
            //deleteOne.Deleter = _httpUser.Name;
            //deleteOne.DeleteTime = DateTime.Now;
            deleteOne.DeleterID = "admind";
            deleteOne.Deleter = "admind";
            deleteOne.DeleteTime = DateTime.Now;

            resp.Data = await _service.Update(deleteOne);

            resp.Success = true;
            resp.Msg = "OK";
            return resp;
        }
    }
}