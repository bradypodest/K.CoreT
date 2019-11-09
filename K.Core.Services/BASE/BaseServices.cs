using K.Core.Services;
using K.Core.IRepository.Base;
using K.Core.IServices.BASE;
using K.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using K.Core.Common.Helper;
using K.Core.Common.HttpContextUser;
using K.Core.Common.Model;
using AutoMapper;

namespace K.Core.Services.BASE
{
    public class BaseServices<TEntity> : ServiceFunFilter<TEntity>
        , IBaseServices<TEntity> 
        where TEntity : BaseExtendTwoEntity, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> baseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        protected IUser _httpUser;

        protected IMapper _mapper;

        public async Task<TEntity> QueryById(object objId)
        {
            return await baseDal.QueryById(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await baseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await baseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await baseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await baseDal.Update(entity);
        }
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await baseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }



        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await baseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return await baseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await baseDal.QueryPage(whereExpression,
         intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            return await baseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
        }




        //----------------------------
        /// <summary>
        /// 分页查询 且 参数动态
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="pageDataOptions"></param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, PageDataOptions pageDataOptions)
        {
            var oLamadaExtention = new LamadaExtention<TEntity>();

            if (pageDataOptions.IsAll)
            {
            }
            else 
            {
                oLamadaExtention.GetExpression("Status", StatusE.Delete, ExpressionType.NotEqual);
            }

            ////循环判断 : 只能处理 = ,like , !=  , > , <,  >=, <=
            //string[] wheres = new string[] { };
            //if (!string.IsNullOrWhiteSpace(pageDataOptions.Wheres)) 
            //{
            //    wheres = pageDataOptions.Wheres.Split("|");
            //}
            //foreach (var item in wheres)
            //{
            //    if (!string.IsNullOrWhiteSpace(item)) 
            //    {
            //        string[] oneWhere = item.Split(",");

            //        oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], (ExpressionType)(oneWhere[1].ToInt()));
            //    }
            //}


            #region  查询参数     时间类型的可能还是有点 问题
            //循环判断 : 只能处理 = ,like , !=  , > , <,  >=, <=
            string[] wheres = new string[] { };
            if (!string.IsNullOrWhiteSpace(pageDataOptions.Wheres))
            {
                wheres = pageDataOptions.Wheres.Split("|");
            }
            foreach (var item in wheres)
            {
                //如果有特殊情况就需要特殊判断 ： 如情况为 查找一个字符串包含在ID  或者  在Name中


                if (!string.IsNullOrWhiteSpace(item))
                {
                    string[] oneWhere = item.Split(",");

                    //oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], (ExpressionType)(oneWhere[1].ToInt()));
                    oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], oneWhere[1].ToEnum<ExpressionType>());
                }
            }

            #endregion

            var lamada = oLamadaExtention.GetLambda();
            
            pageDataOptions.Order=!string.IsNullOrWhiteSpace(pageDataOptions.Order)?pageDataOptions.Order:"CreateTime desc";

            if (lamada != null)
            {
                return await baseDal.QueryPage(lamada,
        pageDataOptions.Page, pageDataOptions.Rows, pageDataOptions.Order);
            }
            else 
            {
                return await baseDal.QueryPage(whereExpression,
       pageDataOptions.Page, pageDataOptions.Rows, pageDataOptions.Order);
            }
               

        }


        //public  MessageModel<bool> Upload(List<IFormFile> files)
        //{
        //    return new MessageModel<bool>() { Success = false, Msg = " 文件上传功能开发中....", Data = false };
        //}

        //public Task<MessageModel<bool>> DownLoadTemplate()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<MessageModel<bool>> Import(List<IFormFile> files)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="pageData"></param>
        /// <returns></returns>
        public virtual Task<MessageModel<bool>> Export(PageDataOptions pageData)
        {
            throw new NotImplementedException();
            //pageData.Export = true;
            ////List<TEntity> list = GetPageData(pageData).rows;
            ////List<TEntity> list = GetPageData(pageData).rows;//查询数据
            //string tableName = typeof(TEntity).GetEntityTableCnName();
            //string fileName = tableName + DateTime.Now.ToString("yyyyMMddHHssmm") + ".xlsx";
            //string folder = DateTime.Now.ToString("yyyyMMdd");
            //string savePath = $"Download/ExcelExport/{folder}/".MapPath();
            //List<string> ignoreColumn = new List<string>();
            //if (ExportOnExecuting != null)
            //{
            //    Response = ExportOnExecuting(list, ignoreColumn);
            //    if (!Response.Status) return Response;
            //}
            //EPPlusHelper.Export(list, ignoreColumn, savePath, fileName);
            //return Response.OK(null, (savePath + "/" + fileName).EncryptDES(AppSetting.Secret.ExportFile));




        }

        #region  增删改查*****
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<MessageModel<int>> AddOne(TEntity t)
        {

            t.CreateID = _httpUser.ID ?? "";
            t.CreateTime = DateTime.Now;
            t.Creator = _httpUser.Name ?? "";

           
            var  data = await baseDal.Add(t);//全局异常拦截应该就不需要 try 

            return MessageModel<int>.Success(data);
        }

        public virtual async Task<MessageModel<bool>> DeleteOne(string ID)
        {
            TEntity deleteOne = await baseDal.QueryById(ID);

            //标志删除
            deleteOne.Status = StatusE.Delete;
            deleteOne.DeleterID = _httpUser.ID;
            deleteOne.Deleter = _httpUser.Name;
            deleteOne.DeleteTime = DateTime.Now;
            

            var data = await baseDal.Update(deleteOne);

            return MessageModel<bool>.Success(true);
        }

        public virtual async Task<MessageModel<bool>> UpdateOne(TEntity t)
        {
            t.Modifier = _httpUser.Name;
            t.ModifyID = _httpUser.ID;
            t.ModifyTime = DateTime.Now;

            List<string> lstIgnoreColumns = new List<string>();
            lstIgnoreColumns.Add("ID");
            lstIgnoreColumns.Add("Status");
            lstIgnoreColumns.Add("Creator");
            lstIgnoreColumns.Add("CreateID");
            lstIgnoreColumns.Add("CreateTime");

            var data = await baseDal.Update(t, null, lstIgnoreColumns, "ID=" + t.ID);

            return MessageModel<bool>.Success(true);
        }

        public virtual async Task<MessageModel<TEntity>> GetOneByID(string id)
        {
            var data = await baseDal.QueryById(id);

            return MessageModel<TEntity>.Success(data);
        }

        public virtual async Task<MessageModel<PageModel<TEntity>>> GetPageData(PageDataOptions pageDataOptions)
        {
            var oLamadaExtention = new LamadaExtention<TEntity>();

            if (pageDataOptions.IsAll)
            {
            }
            else
            {
                oLamadaExtention.GetExpression("Status", StatusE.Delete, ExpressionType.NotEqual);
            }

            #region  查询参数     时间类型的可能还是有点 问题
            //循环判断 : 只能处理 = ,like , !=  , > , <,  >=, <=
            string[] wheres = new string[] { };
            if (!string.IsNullOrWhiteSpace(pageDataOptions.Wheres))
            {
                wheres = pageDataOptions.Wheres.Split("|");
            }
            foreach (var item in wheres)
            {
                //如果有特殊情况就需要特殊判断 ： 如情况为 查找一个字符串包含在ID  或者  在Name中


                if (!string.IsNullOrWhiteSpace(item))
                {
                    string[] oneWhere = item.Split(",");

                    //oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], (ExpressionType)(oneWhere[1].ToInt()));
                    oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], oneWhere[1].ToEnum<ExpressionType>());
                }
            }

            #endregion

            var lamada = oLamadaExtention.GetLambda();

            pageDataOptions.Order = !string.IsNullOrWhiteSpace(pageDataOptions.Order) ? pageDataOptions.Order : "CreateTime desc";

            

            if (lamada != null)
            {
                var data= await baseDal.QueryPage(lamada,
        pageDataOptions.Page, pageDataOptions.Rows, pageDataOptions.Order);

                return MessageModel<PageModel<TEntity>>.Success(data);
            }
            else
            {
                var data= await baseDal.QueryPage(LambdaHelper.True<TEntity>(),
       pageDataOptions.Page, pageDataOptions.Rows, pageDataOptions.Order);

                return MessageModel<PageModel<TEntity>>.Success(data);
            }

        }
        #endregion
    }

}
