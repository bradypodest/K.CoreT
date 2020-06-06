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
using K.Core.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SqlSugar;
using K.Core.IServices.Test;
using K.Core.Services.Test;
using K.Core.Common.Helper.AutofacManager;
using K.Core.Model.ViewModels;
using K.Core.AutoMapper;
using K.Core.Model.ViewModels.Test;
using System.Linq;
using K.Core.Const;
using K.Core.Enums;

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
        /// 作　　者:K.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await baseDal.QueryByIDs(lstIds);
        }

        // /// <summary>
        // /// 写入实体数据
        // /// </summary>
        // /// <param name="entity">博文实体类</param>
        // /// <returns></returns>
        // public async Task<int> Add(TEntity entity)
        // {
        //     return await baseDal.Add(entity);
        // }

        // /// <summary>
        // /// 更新实体数据
        // /// </summary>
        // /// <param name="entity">博文实体类</param>
        // /// <returns></returns>
        // public async Task<bool> Update(TEntity entity)
        // {
        //     return await baseDal.Update(entity);
        // }
        // public async Task<bool> Update(TEntity entity, string strWhere)
        // {
        //     return await baseDal.Update(entity, strWhere);
        // }

        // public async Task<bool> Update(
        //  TEntity entity,
        //  List<string> lstColumns = null,
        //  List<string> lstIgnoreColumns = null,
        //  string strWhere = ""
        //     )
        // {
        //     return await baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        // }


        // /// <summary>
        // /// 根据实体删除一条数据
        // /// </summary>
        // /// <param name="entity">博文实体类</param>
        // /// <returns></returns>
        // public async Task<bool> Delete(TEntity entity)
        // {
        //     return await baseDal.Delete(entity);
        // }

        // /// <summary>
        // /// 删除指定ID的数据
        // /// </summary>
        // /// <param name="id">主键ID</param>
        // /// <returns></returns>
        // public async Task<bool> DeleteById(object id)
        // {
        //     return await baseDal.DeleteById(id);
        // }

        // /// <summary>
        // /// 删除指定ID集合的数据(批量删除)
        // /// </summary>
        // /// <param name="ids">主键ID集合</param>
        // /// <returns></returns>
        // public async Task<bool> DeleteByIds(object[] ids)
        // {
        //     return await baseDal.DeleteByIds(ids);
        // }



        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:K.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:K.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:K.Core
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
        /// 作　　者:K.Core
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
        /// 作　　者:K.Core
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
        /// 作　　者:K.Core
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
        /// 作　　者:K.Core
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
        /// 作　　者:K.Core
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

        // public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        // int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        // {
        //     return await baseDal.QueryPage(whereExpression,
        //  intPageIndex, intPageSize, strOrderByFileds);
        // }

        // public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        // {
        //     return await baseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
        // }




        // //----------------------------
        // /// <summary>
        // /// 分页查询 且 参数动态
        // /// </summary>
        // /// <param name="whereExpression"></param>
        // /// <param name="pageDataOptions"></param>
        // /// <returns></returns>
        // public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, PageDataOptions pageDataOptions)
        // {
        //     var oLamadaExtention = new LamadaExtention<TEntity>();

        //     if (pageDataOptions.IsAll)
        //     {
        //     }
        //     else
        //     {
        //         oLamadaExtention.GetExpression("Status", StatusE.Delete, ExpressionType.NotEqual);
        //     }

        //     ////循环判断 : 只能处理 = ,like , !=  , > , <,  >=, <=
        //     //string[] wheres = new string[] { };
        //     //if (!string.IsNullOrWhiteSpace(pageDataOptions.Wheres)) 
        //     //{
        //     //    wheres = pageDataOptions.Wheres.Split("|");
        //     //}
        //     //foreach (var item in wheres)
        //     //{
        //     //    if (!string.IsNullOrWhiteSpace(item)) 
        //     //    {
        //     //        string[] oneWhere = item.Split(",");

        //     //        oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], (ExpressionType)(oneWhere[1].ToInt()));
        //     //    }
        //     //}


        //     #region  查询参数     时间类型的可能还是有点 问题
        //     //循环判断 : 只能处理 = ,like , !=  , > , <,  >=, <=
        //     string[] wheres = new string[] { };
        //     if (!string.IsNullOrWhiteSpace(pageDataOptions.Wheres))
        //     {
        //         wheres = pageDataOptions.Wheres.Split("|");
        //     }
        //     foreach (var item in wheres)
        //     {
        //         //如果有特殊情况就需要特殊判断 ： 如情况为 查找一个字符串包含在ID  或者  在Name中


        //         if (!string.IsNullOrWhiteSpace(item))
        //         {
        //             string[] oneWhere = item.Split(",");

        //             //oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], (ExpressionType)(oneWhere[1].ToInt()));
        //             oLamadaExtention.GetExpression(oneWhere[0], oneWhere[2], oneWhere[1].ToEnum<ExpressionType>());
        //         }
        //     }

        //     #endregion

        //     var lamada = oLamadaExtention.GetLambda();

        //     pageDataOptions.Order = !string.IsNullOrWhiteSpace(pageDataOptions.Order) ? pageDataOptions.Order : "CreateTime desc";

        //     if (lamada != null)
        //     {
        //         return await baseDal.QueryPage(lamada,
        // pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);
        //     }
        //     else
        //     {
        //         return await baseDal.QueryPage(whereExpression,
        //pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);
        //     }


        // }


        // //public  MessageModel<bool> Upload(List<IFormFile> files)
        // //{
        // //    return new MessageModel<bool>() { Success = false, Msg = " 文件上传功能开发中....", Data = false };
        // //}

        // //public Task<MessageModel<bool>> DownLoadTemplate()
        // //{
        // //    throw new NotImplementedException();
        // //}

        // //public Task<MessageModel<bool>> Import(List<IFormFile> files)
        // //{
        // //    throw new NotImplementedException();
        // //}

        // /// <summary>
        // /// 导出表格
        // /// </summary>
        // /// <param name="pageData"></param>
        // /// <returns></returns>
        // public virtual Task<MessageModel<bool>> Export(PageDataOptions pageData)
        // {
        //     throw new NotImplementedException();
        //     //pageData.Export = true;
        //     ////List<TEntity> list = GetPageData(pageData).rows;
        //     ////List<TEntity> list = GetPageData(pageData).rows;//查询数据
        //     //string tableName = typeof(TEntity).GetEntityTableCnName();
        //     //string fileName = tableName + DateTime.Now.ToString("yyyyMMddHHssmm") + ".xlsx";
        //     //string folder = DateTime.Now.ToString("yyyyMMdd");
        //     //string savePath = $"Download/ExcelExport/{folder}/".MapPath();
        //     //List<string> ignoreColumn = new List<string>();
        //     //if (ExportOnExecuting != null)
        //     //{
        //     //    Response = ExportOnExecuting(list, ignoreColumn);
        //     //    if (!Response.Status) return Response;
        //     //}
        //     //EPPlusHelper.Export(list, ignoreColumn, savePath, fileName);
        //     //return Response.OK(null, (savePath + "/" + fileName).EncryptDES(AppSetting.Secret.ExportFile));




        // }

        #region  增删改查*****
        /// <summary>
        ///  增加 （不包括 详细表）
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task<MessageModel<bool>> AddOne(TEntity t)
        {
            var messageModel = MessageModel<bool>.Fail();

            if (AddOnExecute != null) {//新增前验证  ：如是否已经存在过                
                messageModel = await AddOnExecute(t);
                if (!messageModel.success) return messageModel;
            }

            t = SetAddDefaultVal<TEntity>(t);

            //t.CreateID = _httpUser.ID ?? "";
            //t.CreateTime = DateTime.Now;
            //t.Creator = _httpUser.Name ?? "";

            //t.Status = StatusE.Live;

            //if (string.IsNullOrWhiteSpace(t.ID))
            //{
            //    t.ID = Guid.NewGuid().ToString();
            //}

            var data = await baseDal.Add(t);//全局异常拦截应该就不需要 try 
            if (data > 0)
            {
               return  MessageModel<bool>.Success(true);
            }
            else 
            {
               return MessageModel<bool>.Success(false,"新增失败");
            }
           
        }

        public virtual async Task<MessageModel<bool>> DeleteOne(string ID)
        {
            TEntity deleteOne = await baseDal.QueryById(ID);

            if (deleteOne == null || deleteOne.Status== StatusE.Delete) 
            {
                return MessageModel<bool>.Fail(false,"对应数据不存在,操作失败");
            }

            //查询删除条件
            var messageModel = MessageModel<bool>.Fail();
            if (DelOnExecute != null)
            {//删除前验证                 
                messageModel = await DelOnExecute(deleteOne);
                if (!messageModel.success) return messageModel;
            }

            ////删除的操作   :待  以后  
            //if (DelOnExecuting != null)
            //{

            //}
            //else 
            //{
                
            //}

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
            //查询是否该对象已被删除
            if (baseDal.QueryById(t.ID) == null)
            {
                return MessageModel<bool>.Fail("该对象已不存在，无法修改");
            }

            t = SetUpdateDefaultVal<TEntity>(t);

            //t.Modifier = _httpUser.Name;
            //t.ModifyID = _httpUser.ID;
            //t.ModifyTime = DateTime.Now;


            List<string> lstIgnoreColumns = UpdateIgnoreDefaultField();//忽略项

            //var data = await baseDal.Update(t, null, lstIgnoreColumns, "ID='" + t.ID+"'");
            var data = await baseDal.Update(t, null, lstIgnoreColumns);

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
                var data = await baseDal.QueryPage(lamada,
        pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);

                return MessageModel<PageModel<TEntity>>.Success(data);
            }
            else
            {
                var data = await baseDal.QueryPage(LambdaHelper.True<TEntity>(),
       pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);

                return MessageModel<PageModel<TEntity>>.Success(data);
            }

        }
        #endregion


        public Task<MessageModel<bool>> UseTranAsync(Action action)
        {
            //var result = await baseDal.UseTran(() => action());
            //if (result) 
            //{
            //    return  MessageModel<bool>.Success();
            //}
            //return  MessageModel<bool>.Fail();

            throw new NotImplementedException();
        }


        public MessageModel<bool> UseTran(Action action)
        {
            var result =  baseDal.UseTran(() => action());
            if (result)
            {
                return MessageModel<bool>.Success();
            }
            return MessageModel<bool>.Fail();
        }

        //public virtual async Task<MessageModel<object>> GetDetailPageData(PageDataOptions pageDataOptions)
        //{
        //    var messageModel = MessageModel<object>.Fail();

        //    Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改
        //    if (detailType == null)
        //    {
        //        return MessageModel<object>.Fail("该实体无详细表,查询失败!");
        //    }

        //    var obj = await typeof(BaseServices<TEntity>)
        //         .GetMethod("GetDetailPage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        //         .MakeGenericMethod(new Type[] { detailType }).Invoke(this, new object[] { pageDataOptions });

        //    //object obj = Activator.CreateInstance(BaseServices<detailType>);

        //    if (obj != null)
        //    {
        //        return (MessageModel<object>)obj;
        //    }
        //    else 
        //    {
        //        return messageModel;
        //    }
        //}

        //private PageGridData<Detail> GetDetailPage<Detail>(PageDataOptions options) where Detail : class
        //{
        //    //校验查询值，排序字段，分页大小规则待完
        //    PageGridData<Detail> gridData = new PageGridData<Detail>();
        //    if (options.Value == null) return gridData;
        //    //主表主键字段
        //    string keyName = typeof(T).GetKeyName();

        //    //生成查询条件
        //    Expression<Func<Detail, bool>> whereExpression = keyName.CreateExpression<Detail>(options.Value, LinqExpressionType.Equal);

        //    var queryeable = repository.DbContext.Set<Detail>().Where(whereExpression);

        //    gridData.total = queryeable.Count();

        //    string sortName = options.Sort ?? typeof(Detail).GetKeyName();
        //    Dictionary<string, QueryOrderBy> orderBy = new Dictionary<string, QueryOrderBy>() { {
        //             sortName,
        //             options.Order == "asc" ?
        //             QueryOrderBy.Asc :
        //             QueryOrderBy.Desc } };
        //    gridData.rows = queryeable
        //         .GetIQueryableOrderBy(orderBy)
        //        .Skip((options.Page - 1) * options.Rows)
        //        .Take(options.Rows)
        //        .ToList();
        //    gridData.summary = GetDetailSummary<Detail>(queryeable);
        //    return gridData;
        //}

        private async Task<PageModel<Detail>>  GetDetailPage<Detail>(PageDataOptions pageDataOptions) where Detail : class,new ()
        {
            PageModel<Detail> gridData = new PageModel<Detail>();

            //校验查询值，排序字段，分页大小规则待完
            //PageModel<Detail> gridData = new PageModel<Detail>();
            if (pageDataOptions.Value == null)
            {
                return gridData;
            }
            //主表主键字段
            string keyName = typeof(TEntity).GetKeyName();

            //生成查询条件
            //Expression<Func<Detail, bool>> whereExpression = keyName.CreateExpression<Detail>(options.Value, LinqExpressionType.Equal);

            //var queryeable = repository.DbContext.Set<Detail>().Where(whereExpression);

            //gridData.total = queryeable.Count();

            //string sortName = options.Sort ?? typeof(Detail).GetKeyName();
            //Dictionary<string, QueryOrderBy> orderBy = new Dictionary<string, QueryOrderBy>() { {
            //         sortName,
            //         options.Order == "asc" ?
            //         QueryOrderBy.Asc :
            //         QueryOrderBy.Desc } };
            //gridData.rows = queryeable
            //     .GetIQueryableOrderBy(orderBy)
            //    .Skip((options.Page - 1) * options.Rows)
            //    .Take(options.Rows)
            //    .ToList();
            //gridData.summary = GetDetailSummary<Detail>(queryeable);
            //return gridData;


            var oLamadaExtention = new LamadaExtention<Detail>();

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
                try
                {

               
                int totalCount = 0;
                    var DBo = baseDal.dbContext.Db;

                    var list = DBo.Queryable<Detail>()
                     .OrderByIF(!string.IsNullOrEmpty(pageDataOptions.Order), pageDataOptions.Order)
                     .WhereIF(lamada != null, lamada)
                     .ToPageList(pageDataOptions.PageIndex, pageDataOptions.PageSize, ref totalCount);

                    //var list= await baseDal.dbContext.Db.Queryable<Detail>()
                    // .OrderByIF(!string.IsNullOrEmpty(pageDataOptions.Order), pageDataOptions.Order)
                    // .WhereIF(lamada != null, lamada)
                    // .ToPageListAsync(pageDataOptions.PageIndex, pageDataOptions.PageSize, totalCount);

                    int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / pageDataOptions.PageSize.ObjToDecimal())).ObjToInt();

                    //var detailContext = (DbContext)baseDal.DbContextObject;

                    //detailContext.SetEn

                    //detailContext.GetEntityDB<Detail>(detailContext.Db);


                    //        var data = await baseDal.QueryPage(lamada,
                    //pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);

                    var pagem = new PageModel<Detail>() { dataCount = totalCount, pageCount = pageCount, pageIndex = pageDataOptions.PageIndex, pageSize = pageDataOptions.PageSize, data = list };

               return pagem;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                // var data = await baseDal.QueryPage(LambdaHelper.True<TEntity>(),
                //pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);

                //return MessageModel<PageModel<Detail>>.Success(data, "OK");

                int totalCount = 0;
                var list = await baseDal.dbContext.Db.Queryable<Detail>()
                 .OrderByIF(!string.IsNullOrEmpty(pageDataOptions.Order), pageDataOptions.Order)
                 .WhereIF(LambdaHelper.True<Detail>() != null, LambdaHelper.True<Detail>())
                 .ToPageListAsync(pageDataOptions.PageIndex, pageDataOptions.PageSize, totalCount);

                int pageCount = (Math.Ceiling(totalCount.ObjToDecimal() / pageDataOptions.PageSize.ObjToDecimal())).ObjToInt();

                return new PageModel<Detail>() { dataCount = totalCount, pageCount = pageCount, pageIndex = pageDataOptions.PageIndex, pageSize = pageDataOptions.PageSize, data = list };
            }

            //return gridData;


        }

        public object GetDetailPageData(PageDataOptions pageDataOptions)
        {
            Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改

            if (detailType == null)
            {
                return MessageModel<object>.Fail("该实体无详细表,查询失败!");
            }

            object obj =  typeof(BaseServices<TEntity>)
                .GetMethod("GetDetailPage", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(new Type[] { detailType }).Invoke(this, new object[] { pageDataOptions });
            var obj2 = obj.GetType().GetProperty("Result").GetValue(obj, null);
            return obj2;
        }

        //**************************************   子表功能加入 之后

        #region  赋值方法
        /// <summary>
        /// 给更新操作的 实体类 赋 某些默认值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TSource SetUpdateDefaultVal<TSource>(TSource source)
        {
            foreach (PropertyInfo property in typeof(TSource).GetProperties())
            {
                string filed = property.Name.ToLower();

                //t.Modifier = _httpUser.Name;
                //t.ModifyID = _httpUser.ID;
                //t.ModifyTime = DateTime.Now;

                switch (filed)
                {
                    case "modifyid":
                        property.SetValue(source, _httpUser.ID);
                        break;
                    case "modifier":
                        property.SetValue(source, _httpUser.Name);
                        break;
                    case "modifytime":
                        property.SetValue(source, DateTime.Now);
                        break;
                    default :
                        break;
                }
            }
            return source;
        }

        /// <summary>
        /// 给新增操作的实体类 赋 某些默认值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TSource SetAddDefaultVal<TSource>(TSource source) 
        {
            foreach (PropertyInfo property in typeof(TSource).GetProperties())
            {
                string filed = property.Name.ToLower();

                //t.CreateID = _httpUser.ID ?? "";
                //t.CreateTime = DateTime.Now;
                //t.Creator = _httpUser.Name ?? "";

                switch (filed)
                {
                    case "id":
                        property.SetValue(source, Guid.NewGuid().ToString());
                        break;
                    case "status":
                        property.SetValue(source, StatusE.Live);
                        break;
                    case "createid":
                        property.SetValue(source, _httpUser.ID);
                        break;
                    case "creator":
                        property.SetValue(source, _httpUser.Name);
                        break;
                    case "createtime":
                        property.SetValue(source, DateTime.Now);
                        break;
                    default:
                        break;
                }
            }
            return source;
        }

        /// <summary>
        /// 给删除操作的实体类 赋 某些默认值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TSource SetDelDefaultVal<TSource>(TSource source) 
        {
            foreach (PropertyInfo property in typeof(TSource).GetProperties())
            {
                string filed = property.Name.ToLower();

                //标志删除
                //deleteOne.Status = StatusE.Delete;
                //deleteOne.DeleterID = _httpUser.ID;
                //deleteOne.Deleter = _httpUser.Name;
                //deleteOne.DeleteTime = DateTime.Now;

                switch (filed)
                {
                    case "deleterid":
                        property.SetValue(source, _httpUser.ID);
                        break;
                    case "deleter":
                        property.SetValue(source, _httpUser.Name);
                        break;
                    case "deletetime":
                        property.SetValue(source, DateTime.Now);
                        break;
                    case "status":
                        property.SetValue(source, StatusE.Delete);
                        break;
                    default:
                        break;
                }
            }
            return source;
        }

        /// <summary>
        /// 更新忽略的字段
        /// </summary>
        /// <returns></returns>
        public List<string> UpdateIgnoreDefaultField() 
        {
            List<string> lstIgnoreColumns = new List<string>();//忽略项
            //lstIgnoreColumns.Add("ID");
            lstIgnoreColumns.Add("Status");

            lstIgnoreColumns.Add("Creator");
            lstIgnoreColumns.Add("CreateID");
            lstIgnoreColumns.Add("CreateTime");

            lstIgnoreColumns.Add("Deleter");
            lstIgnoreColumns.Add("DeleterID");
            lstIgnoreColumns.Add("DeleteTime");

            return lstIgnoreColumns;
        }
        #endregion

        #region   父 子 表 更新 (子表现阶段只有一个)
        public object UpdateT<SaveModel>(SaveModel saveModel) where SaveModel:SaveModelVM<TEntity>
        {
            //将数据转化为T
            //var source = new Source<TEntity> { Value = saveModel.MainData };
            //var t = _mapper.Map<Destination<TEntity>>(source);//获取主表数据
            //TEntity te = t.Value;
            TEntity te = saveModel.MainData;

            if (te != null)
            {
                //检验主表 值是否输入完整
                string reslut = typeof(TEntity).ValidateInEntity(te, typeof(TEntity).GetProperties(), true, new string[] { "RefID" });//RefID 是子表的外键 忽略掉
                if (reslut != string.Empty)
                    return MessageModel<bool>.Fail(reslut);

                Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改


                if (!string.IsNullOrWhiteSpace(te.ID)) 
                {
                    if (detailType != null)
                    {
                        //验证主表数据
                        object obj = typeof(BaseServices<TEntity>)
                           .GetMethod("UpdateTD", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                           .MakeGenericMethod(new Type[] { detailType, typeof(SaveModel) }).Invoke(this, new object[] { te, saveModel });
                        var obj2 = obj.GetType().GetProperty("Result").GetValue(obj, null);
                        return obj2;
                    }
                    else 
                    {
                        return   UpdateOne(te);
                    }
                        
                }
            }


            return MessageModel<bool>.Fail(false, "无主表数据");

        }

        public async Task<object> UpdateTD<Detail, SaveModel>(TEntity te, SaveModel saveModel) where Detail : BaseExtendTwoEntity, new()
        {
            PropertyInfo detailsPro = typeof(SaveModel).GetDetailsProperty();

            //var ss = detailsPro.GetValue(tvm);
            //var source= new Source<List<object>> {Value= detailsPro.GetValue(saveModel) as List<object> };
            //var destination = _mapper.Map<Destination<List<Detail>>>(source);
            //List<Detail> details = destination.Value;// 无法转换，int double 类型无法转换，只能转换string类型的


            //List<Detail> details = detailsPro.GetValue(saveModel) as List<Detail>;//这样转换会为null
            List<object> detailsObject = detailsPro.GetValue(saveModel) as List<object>;
            List<Detail> details = new List<Detail>();
            foreach (var item in detailsObject)
            {
                //object e = new {ss="ss"};
                //var i = item as Detail;
                //var i=item.ConvertEntity<Detail>();//这里的item 是这种格式{{}}，不知道为啥
                try
                {
                    var i = JsonHelper.JsonToEntity<Detail>(item.ToString());
                    details.Add(i);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
               
            }       
           

            List<Detail> adds = details.Where(c => string.IsNullOrWhiteSpace(c.ID)).ToList();//新增的无id

            List<Detail> updates = details.Where(c => !string.IsNullOrWhiteSpace(c.ID)).ToList();//更新有ID

            PropertyInfo delKeysPro = typeof(SaveModel).GetProperties().Where(c => "DelKeys".Equals(c.Name)).FirstOrDefault();
            List<object> delKeys = delKeysPro.GetValue(saveModel) as List<object>;//删除

            if ((details == null || details.Count == 0) && (delKeys == null || delKeys.Count == 0))
            {
                //没有子表
                return await UpdateOne(te);
            }

            //主表查询     //查询是否该对象已被删除
            if (baseDal.QueryById(te.ID) == null)
            {
                return MessageModel<bool>.Fail("该对象已不存在，无法修改");
            }
            //设置主表某些默认值
            te = SetUpdateDefaultVal(te);

            //设置子表默认值
            //新增情况下
            List<Detail> addsDetailEntity = new List<Detail>();
            PropertyInfo RefKeyPro = typeof(Detail).GetProperties().Where(c => "RefID".Equals(c.Name)).FirstOrDefault();
            foreach (var item in adds)
            {
                //设置子表外键
                RefKeyPro.SetValue(item, te.ID);      //更新主表功能的时候主表有ID
                addsDetailEntity.Add(SetAddDefaultVal(item));
            }

            //修改情况下
            List<Detail> updatesDetailEntity = new List<Detail>();
            foreach (var item in updates)
            {
                updatesDetailEntity.Add(SetUpdateDefaultVal(item));
            }

            //检测 子表中数据 该必填的填了
            string reslut = typeof(Detail).ValidateInListEntity(addsDetailEntity, true, new string[] {"RefID" });//RefID 是子表的外键 忽略掉
            if (reslut != string.Empty)
                return MessageModel<bool>.Fail(reslut);


            if (baseDal.UseTran(() =>
            {
                //删除情况下
                var DBo = baseDal.dbContext.Db;
                //查询出需要删除的子表数据
                List<Detail> dels =  DBo.Queryable<Detail>().In(delKeys).ToList();
                List<Detail> delsDetailEntity = new List<Detail>();
                foreach (var item in dels)
                {
                    delsDetailEntity.Add(SetDelDefaultVal(item));//删除实体类设置默认值之后，下面操作就是修改了，本系统删除是软删除
                }
                //上方代码是否可以替换为  反射

                //更新主表
                baseDal.Update(te, null, UpdateIgnoreDefaultField());

                //更新子表
                foreach (var item in updatesDetailEntity)
                {
                    IUpdateable<Detail> up = DBo.Updateable(item);
                    //up = up.IgnoreColumns(it => UpdateIgnoreDefaultField().Contains(it));//这个弃用了,不知道为何可以用
                    up = up.IgnoreColumns(UpdateIgnoreDefaultField().ToArray());//妈的，上面的一用就有可能出问题

                    up.ExecuteCommandHasChange();
                }
                //更新子表删除
                foreach (var item in delsDetailEntity)
                {
                    //应该是从数据库查询的，所以不需要忽略一些字段
                    DBo.Updateable(item).ExecuteCommandHasChange();
                }
                //子表新增
                //剔除掉数据中参数未必填的数据 ，如 有的  Name 字段是必填属性，但是数据为空 ，则剔除掉  （思路：实体类上面有 [Required] 特性的）  待修改  
                

                if (addsDetailEntity!=null && addsDetailEntity.Count>0)
                    DBo.Insertable(addsDetailEntity.ToArray()).ExecuteCommand();

            }))
            {
                return MessageModel<bool>.Success(true, "OK");
            }
            else
            {
                return MessageModel<bool>.Fail(false, "保存事务失败");
            }

        }
        #endregion

        #region  父 子 表 新增 （子表现阶段只有一个） 
        public object AddT<SaveModel>(SaveModel saveModel) where SaveModel : SaveModelVM<TEntity>
        {
            TEntity te = saveModel.MainData;

            if (te != null)
            {
                //检验主表值是否输入完整
                string reslut = typeof(TEntity).ValidateInEntity(te, typeof(TEntity).GetProperties(), true, new string[] { "RefID" });//RefID 是子表的外键 忽略掉
                if (reslut != string.Empty)
                    return MessageModel<bool>.Fail(reslut);

                Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改

                if (detailType != null)
                {
                    //验证主表数据
                    object obj = typeof(BaseServices<TEntity>)
                       .GetMethod("AddTD", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                       .MakeGenericMethod(new Type[] { detailType, typeof(SaveModel) }).Invoke(this, new object[] { te, saveModel });
                    var obj2 = obj.GetType().GetProperty("Result").GetValue(obj, null);
                    return obj2;
                }
                else 
                {
                    return AddOne(te);//这个地方需要测试   int 是否可以转 bool
                }
            }
            else
            {
                return MessageModel<bool>.Fail(false, "无主表数据");
            }
        }

        public async Task<object> AddTD<Detail, SaveModel>(TEntity te, SaveModel saveModel) where Detail : BaseExtendTwoEntity, new()
        {
            PropertyInfo detailsPro = typeof(SaveModel).GetDetailsProperty();

            List<object> detailsObject = detailsPro.GetValue(saveModel) as List<object>;
            List<Detail> details = new List<Detail>();
            foreach (var item in detailsObject)
            {
                //object e = new {ss="ss"};
                //var i = item as Detail;
                //var i=item.ConvertEntity<Detail>();//这里的item 是这种格式{{}}，不知道为啥
                try
                {
                    var i = JsonHelper.JsonToEntity<Detail>(item.ToString());
                    details.Add(i);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


            List<Detail> adds = details.Where(c => string.IsNullOrWhiteSpace(c.ID)).ToList();//新增的无id

            if (details == null || details.Count == 0|| adds==null|| adds.Count==0)
            {
                //没有子表
                return await AddOne(te);
            }


            var messageModel = MessageModel<bool>.Fail();
            if (AddOnExecute != null)
            {//新增前验证  ：如是否已经存在过                
                messageModel = await AddOnExecute(te);
                if (!messageModel.success) return messageModel;
            }

            //设置主表某些默认值
            te = SetAddDefaultVal(te);

            //设置子表默认值
            //新增情况下
            List<Detail> addsDetailEntity = new List<Detail>();
            PropertyInfo RefKeyPro = typeof(Detail).GetProperties().Where(c => "RefID".Equals(c.Name)).FirstOrDefault();
            foreach (var item in adds)
            {
                //设置子表外键
                RefKeyPro.SetValue(item, te.ID ?? "");      //新增主表功能的时候主表无ID ,但是前面代码已经赋值

                addsDetailEntity.Add(SetAddDefaultVal(item));
            }

            //检测 子表中数据 该必填的填了
            string reslut = typeof(Detail).ValidateInListEntity(addsDetailEntity, true, new string[] { "RefID" });//RefID 是子表的外键 忽略掉
            if (reslut != string.Empty)
                return MessageModel<bool>.Fail(reslut);


            if (baseDal.UseTran(() =>
            {
                //删除情况下
                var DBo = baseDal.dbContext.Db;
               
                //新增主表
                baseDal.Add(te);

                //子表新增
                //剔除掉数据中参数未必填的数据 ，如 有的  Name 字段是必填属性，但是数据为空 ，则剔除掉  （思路：实体类上面有 [Required] 特性的）  待修改  
                if (addsDetailEntity != null && addsDetailEntity.Count > 0)
                    DBo.Insertable(addsDetailEntity.ToArray()).ExecuteCommand();

            }))
            {
                return MessageModel<bool>.Success(true, "OK");
            }
            else
            {
                return MessageModel<bool>.Fail(false, "保存事务失败");
            }

        }
        #endregion


        #region  升级版查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageDataOptions"></param>
        /// <returns></returns>
        public virtual async Task<MessageModel<PageModel<TEntity>>> GetPageDataT(PageDataOptions pageDataOptions)
        {
            //var oLamadaExtention = new LamadaExtention<TEntity>();
            List<Expression<Func<TEntity, bool>>> whereExpressions = new List<Expression<Func<TEntity, bool>>>();

            if (pageDataOptions.IsAll)
            {
            }
            else
            {
                //oLamadaExtention.GetExpression("Status", StatusE.Delete, ExpressionType.NotEqual);
                whereExpressions.Add("Status".CreateExpression<TEntity>(StatusE.Delete, LinqExpressionType.NotEqual));
            }

            //将条件反序列化
            List<SearchParameters> searchParametersList = new List<SearchParameters>();
            if (!string.IsNullOrEmpty(pageDataOptions.Wheres))
            {
                try
                {
                    searchParametersList = pageDataOptions.Wheres.DeserializeObject<List<SearchParameters>>();
                }
                catch { }
            }

            QueryRelativeList?.Invoke(searchParametersList);//查询前可以在继承类中 扩展条件

            foreach (var item in searchParametersList)
            {
                item.DisplayType = item.DisplayType.GetDBCondition();//将文字转为字符，如 thanorequal 转为 >=

                if (string.IsNullOrEmpty(item.Value))
                {
                    continue;
                }

                object[] values = item.Value.Split(',');//这是全部的，没有移除的，
                                                        //验证 数据是否与数据库中的类型是否一致，即 检查数据的正确性  ，不正确的移除  待完成


                if (values == null || values.Length == 0)
                {
                    continue;
                }

                if (item.DisplayType == HtmlElementType.Contains)//如果是in的查询条件，则将值组合起来
                    item.Value = string.Join(",", values);

                K.Core.Enums.LinqExpressionType expressionType = item.DisplayType.GetLinqCondition();

                if (K.Core.Enums.LinqExpressionType.In == expressionType)
                {
                    whereExpressions.Add(item.Name.CreateExpression<TEntity>(values, expressionType));
                } else
                {
                    whereExpressions.Add(item.Name.CreateExpression<TEntity>(item.Value, expressionType));
                }

            }

            if (base.orderbyString != null && !string.IsNullOrWhiteSpace(base.orderbyString))
            {
                pageDataOptions.Order = base.orderbyString;
            }
            else
            {
                pageDataOptions.Order = !string.IsNullOrWhiteSpace(pageDataOptions.Order) ? pageDataOptions.Order : "CreateTime desc";
            }

                var data = await baseDal.QueryPage(whereExpressions,
       pageDataOptions.PageIndex, pageDataOptions.PageSize, pageDataOptions.Order);

                return MessageModel<PageModel<TEntity>>.Success(data);

        }
        #endregion



        #region  //更新 子 父 表（废弃）
        ///// <summary>
        ///// 主表与子表 的新增 （单独保存主表（既无子表的数据）也可以使用该方法）
        ///// </summary>
        ///// <param name="saveModel"></param>
        ///// <returns></returns>
        //[Obsolete]
        //public object Update<TVM>(TVM tvm)
        //{
        //    //将数据转化为T
        //    var source = new Source<TVM> { Value = tvm };
        //    var t = _mapper.Map<Destination<TEntity>>(source);//获取主表数据
        //    TEntity te = t.Value;

        //    Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改

        //    if (te != null && !string.IsNullOrWhiteSpace(te.ID))
        //    {
        //        //验证主表数据
        //        object obj = typeof(BaseServices<TEntity>)
        //           .GetMethod("UpdateD", BindingFlags.Instance | BindingFlags.NonPublic)
        //           .MakeGenericMethod(new Type[] { detailType, typeof(TVM) }).Invoke(this, new object[] { te, tvm });
        //        var obj2 = obj.GetType().GetProperty("Result").GetValue(obj, null);
        //        return obj2;
        //    }
        //    else
        //    {
        //        return MessageModel<bool>.Fail(false, "无主表数据");
        //    }




        //    //var obj2 = obj.GetType().GetProperty("Result").GetValue(obj, null);
        //    //return obj2;


        //}

        //[Obsolete]
        //public async Task<object> UpdateD<Detail, TVM>(TEntity te, TVM tvm) where Detail : BaseExtendTwoEntity, new()
        //{
        //    PropertyInfo detailsPro = typeof(TVM).GetDetailsProperty();

        //    //var ss = detailsPro.GetValue(tvm);

        //    List<Detail> details = detailsPro.GetValue(tvm) as List<Detail>;

        //    List<Detail> adds = details.Where(c => string.IsNullOrWhiteSpace(c.ID)).ToList();//新增的无id

        //    List<Detail> updates = details.Where(c => !string.IsNullOrWhiteSpace(c.ID)).ToList();//更新有ID


        //    PropertyInfo delKeysPro = typeof(TVM).GetProperties().Where(c => "DelKeys".Equals(c.Name)).FirstOrDefault();
        //    List<object> delKeys = delKeysPro.GetValue(tvm) as List<object>;//删除

        //    if ((details == null || details.Count == 0) && (delKeys == null || delKeys.Count == 0))
        //    {
        //        //没有子表
        //        return await UpdateOne(te);
        //    }


        //    //主表查询     //查询是否该对象已被删除
        //    if (baseDal.QueryById(te.ID) == null)
        //    {
        //        return MessageModel<bool>.Fail("该对象已不存在，无法修改");
        //    }
        //    //设置主表某些默认值
        //    te = SetUpdateDefaultVal(te);

        //    //设置子表默认值
        //    //新增情况下
        //    List<Detail> addsDetailEntity = new List<Detail>();
        //    foreach (var item in adds)
        //    {
        //        addsDetailEntity.Add(SetAddDefaultVal(item));
        //    }

        //    //修改情况下
        //    List<Detail> updatesDetailEntity = new List<Detail>();
        //    foreach (var item in updates)
        //    {
        //        updatesDetailEntity.Add(SetUpdateDefaultVal(item));
        //    }

        //    if (baseDal.UseTran(async () =>
        //    {
        //        //删除情况下
        //        var DBo = baseDal.dbContext.Db;
        //        //查询出需要删除的子表数据
        //        List<Detail> dels = await DBo.Queryable<Detail>().In(delKeys).ToListAsync();
        //        List<Detail> delsDetailEntity = new List<Detail>();
        //        foreach (var item in dels)
        //        {
        //            delsDetailEntity.Add(SetDelDefaultVal(item));//删除实体类设置默认值之后，下面操作就是修改了，本系统删除是软删除
        //        }
        //        //上方代码是否可以替换为  反射

        //        //更新主表
        //        await baseDal.Update(te, null, UpdateIgnoreDefaultField());

        //        //更新子表
        //        foreach (var item in updatesDetailEntity)
        //        {
        //            IUpdateable<Detail> up = DBo.Updateable(item);
        //            up = up.IgnoreColumns(it => UpdateIgnoreDefaultField().Contains(it));//这个弃用了,不知道为何可以用

        //            await up.ExecuteCommandHasChangeAsync();
        //        }
        //        //更新子表删除
        //        foreach (var item in delsDetailEntity)
        //        {
        //            //应该是从数据库查询的，所以不需要忽略一些字段
        //            await DBo.Updateable(item).ExecuteCommandHasChangeAsync();
        //        }
        //        //子表新增
        //        await DBo.Insertable(addsDetailEntity.ToArray()).ExecuteCommandAsync();

        //    }))
        //    {
        //        return MessageModel<bool>.Success(true, "OK");
        //    }
        //    else
        //    {
        //        return MessageModel<bool>.Fail(false, "保存事务失败");
        //    }

        //}
        #endregion

        //public virtual async Task<object> GetDetailPageData(PageDataOptions pageDataOptions)
        //{
        //    Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改

        //    if (detailType == null)
        //    {
        //        return MessageModel<object>.Fail("该实体无详细表,查询失败!");
        //    }

        //    var baseServiceDetail = typeof(BaseServices<>).MakeGenericType(detailType);
        //    object e = Activator.CreateInstance(baseServiceDetail);
        //    var obj= baseServiceDetail.GetMethod("GetPageData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(e, new object[] { pageDataOptions });
        //    //dynamic messageModel = Activator.CreateInstance(messageModelT);






        //    //messageModelT.GetMethod("GetPageData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke()

        //    // messageModel

        //    //var obj =  typeof(BaseServices<TEntity>)
        //    //     .GetMethod("GetDetailPage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        //    //     .MakeGenericMethod(new Type[] { detailType }).Invoke(this, new object[] { pageDataOptions });

        //    //object obj = Activator.CreateInstance(BaseServices<detailType>);

        //    if (obj != null)
        //    {
        //        return (MessageModel<object>)obj;
        //    }
        //    else
        //    {
        //        return MessageModel<object>.Fail("查询失败");
        //    }
        //}

        //public async Task<MessageModel<object>> GetDetailPageData(PageDataOptions pageDataOptions)
        //{
        //    Type detailType = typeof(TEntity).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];//局限：只能查询第一个子表  待修改

        //    if (detailType == null)
        //    {
        //        return  MessageModel<object>.Fail("该实体无详细表,查询失败!");
        //    }


        //    //var MethodType = typeof(AutofacContainerModule);
        //    //var GenericMethod = MethodType.GetMethod("GetService");
        //    //MethodInfo curMethod = GenericMethod.MakeGenericMethod(typeof(ITestOrderDetailService));
        //    //object eo= curMethod.Invoke(null, new object[] { });


        //    //Assembly mockAssembly = Assembly.GetExecutingAssembly();



        //    //var baseServiceDetail = typeof(BaseServices<>).MakeGenericType(detailType);


        //    //PropertyInfo property = typeof(detailType).GetKeyProperty();
        //    //object e= property.PropertyType
        //    //    .Assembly
        //    //    .CreateInstance(property.PropertyType.FullName)

        //    //var baseServiceDetail = typeof(TestOrderDetailService);
        //    //object e = Activator.CreateInstance(baseServiceDetail);
        //    //object eo = baseServiceDetail.GetMethod("Instance", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { });

        //    //object e = baseServiceDetail.Assembly.CreateInstance(baseServiceDetail.FullName);

        //    //var obj = (Task)baseServiceDetail.GetMethod("GetPageData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(eo, new object[] { pageDataOptions });
        //    //dynamic messageModel = Activator.CreateInstance(messageModelT);

        //    //await obj;
        //    //var result = obj.GetType().GetProperty("Result").GetValue(obj,null);




        //    //messageModelT.GetMethod("GetPageData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke()

        //    // messageModel

        //    //var obj =  typeof(BaseServices<TEntity>)
        //    //     .GetMethod("GetDetailPage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        //    //     .MakeGenericMethod(new Type[] { detailType }).Invoke(this, new object[] { pageDataOptions });

        //    //object obj = Activator.CreateInstance(BaseServices<detailType>);

        //    //if (obj != null)
        //    //{
        //    //    //return (MessageModel<object>)result;
        //    //    return MessageModel<object>.Success(result, "OK");
        //    //}
        //    //else
        //    //{
        //    //    return MessageModel<object>.Fail("查询失败");
        //    //}

        //    return MessageModel<object>.Fail("查询失败");
        //}





    }

}
