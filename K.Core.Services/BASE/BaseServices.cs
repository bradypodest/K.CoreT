﻿using K.Core.Services;
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

namespace K.Core.Services.BASE
{
    public class BaseServices<TEntity> : ServiceFunFilter<TEntity>
        , IBaseServices<TEntity> 
        where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public async Task<TEntity> QueryById(object objId)
        {
            return await BaseDal.QueryById(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await BaseDal.QueryById(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await BaseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            return await BaseDal.Add(entity);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await BaseDal.Update(entity);
        }
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await BaseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await BaseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await BaseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await BaseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await BaseDal.DeleteByIds(ids);
        }



        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await BaseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await BaseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.K.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
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
            return await BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await BaseDal.Query(whereExpression, strOrderByFileds);
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
            return await BaseDal.Query(strWhere, strOrderByFileds);
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
            return await BaseDal.Query(whereExpression, intTop, strOrderByFileds);
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
            return await BaseDal.Query(strWhere, intTop, strOrderByFileds);
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
            return await BaseDal.Query(
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
            return await BaseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        public async Task<PageModel<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
        int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await BaseDal.QueryPage(whereExpression,
         intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(Expression<Func<T, T2, T3, object[]>> joinExpression, Expression<Func<T, T2, T3, TResult>> selectExpression, Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            return await BaseDal.QueryMuch(joinExpression, selectExpression, whereLambda);
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
                return await BaseDal.QueryPage(lamada,
        pageDataOptions.Page, pageDataOptions.Rows, pageDataOptions.Order);
            }
            else 
            {
                return await BaseDal.QueryPage(whereExpression,
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

        
    }

}
