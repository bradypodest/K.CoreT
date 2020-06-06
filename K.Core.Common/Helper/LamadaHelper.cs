﻿using K.Core.Common.Helper;
using K.Core.Enums;
using K.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace K.Core.Common.Helper
{
    public static class LambdaHelper
    {
        /// <summary>
        /// 创建lambda表达式：p=>true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return p => true;
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {

            return p => false;
        }

        public static ParameterExpression GetExpressionParameter(this Type type)
        {

            return Expression.Parameter(type, "p");
        }
        /// <summary>
        /// 创建lambda表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetExpression<T, TKey>(this string propertyName)
        {
            return propertyName.GetExpression<T, TKey>(typeof(T).GetExpressionParameter());
        }
        /// <summary>
        /// 创建委托有返回值的表达式：p=>p.propertyName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static Func<T, TKey> GetFun<T, TKey>(this string propertyName)
        {
            return propertyName.GetExpression<T, TKey>(typeof(T).GetExpressionParameter()).Compile();
        }

        /// <summary>
        /// 创建lambda表达式：p=>false
        /// 在已知TKey字段类型时,如动态排序OrderBy(x=>x.ID)会用到此功能,返回的就是x=>x.ID
        /// Expression<Func<Out_Scheduling, DateTime>> expression = x => x.CreateDate;指定了类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> GetExpression<T, TKey>(this string propertyName, ParameterExpression parameter)
        {
            if (typeof(TKey).Name == "Object")
                return Expression.Lambda<Func<T, TKey>>(Expression.Convert(Expression.Property(parameter, propertyName), typeof(object)), parameter);
            return Expression.Lambda<Func<T, TKey>>(Expression.Property(parameter, propertyName), parameter);
        }
        /// <summary>
        /// 创建lambda表达式：p=>false
        /// object不能确认字段类型(datetime,int,string),如动态排序OrderBy(x=>x.ID)会用到此功能,返回的就是x=>x.ID
        /// Expression<Func<Out_Scheduling, object>> expression = x => x.CreateDate;任意类型的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, object>> GetExpression<T>(this string propertyName)
        {
            return propertyName.GetExpression<T, object>(typeof(T).GetExpressionParameter());
        }

        public static Expression<Func<T, object>> GetExpression<T>(this string propertyName, ParameterExpression parameter)
        {
            return Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.Property(parameter, propertyName), typeof(object)), parameter);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">字段名</param>
        /// <param name="propertyValue">表达式的值</param>
        /// <param name="expressionType">创建表达式的类型,如:p=>p.propertyName != propertyValue 
        /// p=>p.propertyName.Contains(propertyValue)</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CreateExpression<T>(this string propertyName, object propertyValue, LinqExpressionType expressionType)
        {
            return propertyName.CreateExpression<T>(propertyValue, null, expressionType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">字段名</param>
        /// <param name="propertyValue">表达式的值</param>
        /// <param name="expressionType">创建表达式的类型,如:p=>p.propertyName != propertyValue 
        /// p=>p.propertyName.Contains(propertyValue)</param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> CreateExpression<T>(this string propertyName, object propertyValue, ParameterExpression parameter, LinqExpressionType expressionType)
        {
            Type proType = typeof(T).GetProperty(propertyName).PropertyType;
            //创建节点变量如p=>的节点p
            //  parameter ??= Expression.Parameter(typeof(T), "p");//创建参数p
            parameter = parameter ?? Expression.Parameter(typeof(T), "p");

            //创建节点的属性p=>p.name 属性name
            MemberExpression memberProperty = Expression.PropertyOrField(parameter, propertyName);
            if (expressionType == LinqExpressionType.In)
            {
                if (!(propertyValue is System.Collections.IList list) || list.Count == 0) throw new Exception("属性值类型不正确");

                bool isStringValue = true;
                List<object> objList = new List<object>();

                if (proType.ToString() != "System.String")
                {
                    isStringValue = false;
                    foreach (var value in list)
                    {
                        objList.Add(value.ToString().ChangeType(proType));
                    }
                    list = objList;
                }

                if (isStringValue)
                {
                    //string 类型的字段，如果值带有'单引号,EF会默认变成''两个单引号
                    MethodInfo method = typeof(System.Collections.IList).GetMethod("Contains");
                    //创建集合常量并设置为常量的值
                    ConstantExpression constantCollection = Expression.Constant(list);
                    //创建一个表示调用带参数的方法的：new string[]{"1","a"}.Contains("a");
                    MethodCallExpression methodCall = Expression.Call(constantCollection, method, memberProperty);
                    return Expression.Lambda<Func<T, bool>>(methodCall, parameter);
                }
                //非string字段，按上面方式处理报异常Null TypeMapping in Sql Tree
                BinaryExpression body = null;
                foreach (var value in list)
                {
                    ConstantExpression constantExpression = Expression.Constant(value);
                    UnaryExpression unaryExpression = Expression.Convert(memberProperty, constantExpression.Type);

                    body = body == null
                        ? Expression.Equal(unaryExpression, constantExpression)
                        : Expression.OrElse(body, Expression.Equal(unaryExpression, constantExpression));
                }
                return Expression.Lambda<Func<T, bool>>(body, parameter);
            }

            //  object value = propertyValue;
            ConstantExpression constant = proType.ToString() == "System.String"
                ? Expression.Constant(propertyValue) : Expression.Constant(propertyValue.ToString().ChangeType(proType));

            UnaryExpression member = Expression.Convert(memberProperty, constant.Type);
            Expression<Func<T, bool>> expression;
            switch (expressionType)
            {
                //p=>p.propertyName == propertyValue
                case LinqExpressionType.Equal:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter);
                    break;
                //p=>p.propertyName != propertyValue
                case LinqExpressionType.NotEqual:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.NotEqual(member, constant), parameter);
                    break;
                //   p => p.propertyName > propertyValue
                case LinqExpressionType.GreaterThan:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant), parameter);
                    break;
                //   p => p.propertyName < propertyValue
                case LinqExpressionType.LessThan:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.LessThan(member, constant), parameter);
                    break;
                // p => p.propertyName >= propertyValue
                case LinqExpressionType.ThanOrEqual:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
                    break;
                // p => p.propertyName <= propertyValue
                case LinqExpressionType.LessThanOrEqual:
                    expression = Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
                    break;
                //   p => p.propertyName.Contains(propertyValue)
                // p => !p.propertyName.Contains(propertyValue)
                case LinqExpressionType.Contains:
                case LinqExpressionType.NotContains:
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    constant = Expression.Constant(propertyValue, typeof(string));
                    if (expressionType == LinqExpressionType.Contains)
                    {
                        expression = Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameter);
                    }
                    else
                    {
                        expression = Expression.Lambda<Func<T, bool>>(Expression.Not(Expression.Call(member, method, constant)), parameter);
                    }
                    break;
                default:
                    // p => p.false
                    expression = False<T>();
                    break;
            }
            return expression;
        }


        /// <summary>
        /// 表达式转换成KeyValList(主要用于多字段排序，并且多个字段的排序规则不一样)
        /// 如有多个字段进行排序,参数格式为
        ///  Expression<Func<Out_Scheduling, Dictionary<object, bool>>> orderBy = x => new Dictionary<object, bool>() {
        ///            { x.ID, true },
        ///           { x.DestWarehouseName, true }
        ///      };
        ///      返回的是new Dictionary<object, bool>(){{}}key为排序字段，bool为升降序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, QueryOrderBy>> GetExpressionToPair<T>(this Expression<Func<T, Dictionary<object, QueryOrderBy>>> expression)
        {

            foreach (var exp in ((ListInitExpression)expression.Body).Initializers)
            {
                yield return new KeyValuePair<string, QueryOrderBy>(
                exp.Arguments[0] is MemberExpression ?
                (exp.Arguments[0] as MemberExpression).Member.Name.ToString()
                : ((exp.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member.Name,
                 (QueryOrderBy)((exp.Arguments[1] as ConstantExpression).Value));
            }
        }


        /// <summary>
        /// 表达式转换成KeyValList(主要用于多字段排序，并且多个字段的排序规则不一样)
        /// 如有多个字段进行排序,参数格式为
        ///  Expression<Func<Out_Scheduling, Dictionary<object, QueryOrderBy>>> orderBy = x => new Dictionary<object, QueryOrderBy>() {
        ///            { x.ID, QueryOrderBy.Desc },
        ///           { x.DestWarehouseName, QueryOrderBy.Asc }
        ///      };
        ///      返回的是new Dictionary<object, QueryOrderBy>(){{}}key为排序字段，QueryOrderBy为排序方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Dictionary<string, QueryOrderBy> GetExpressionToDic<T>(this Expression<Func<T, Dictionary<object, QueryOrderBy>>> expression)
        {
            return expression.GetExpressionToPair().ToList().ToDictionary(x => x.Key, x => x.Value);
        }
        /// <summary>
        /// 添加order数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="orderBySelector">string=排序的字段,bool=true降序/false升序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetIQueryableOrderBy<TEntity>(this IQueryable<TEntity> queryable, Dictionary<string, QueryOrderBy> orderBySelector)
        {
            string[] orderByKeys = orderBySelector.Select(x => x.Key).ToArray();

            //   typeof(TEntity).GetProperties().Where(x=>x.)

            for (int i = orderByKeys.Length - 1; i >= 0; i--)
            {
                if (orderBySelector[orderByKeys[i]] == QueryOrderBy.Desc)
                {
                    queryable = queryable.OrderByDescending(orderByKeys[i].GetExpression<TEntity>());
                }
                else
                {
                    queryable = queryable.OrderBy(orderByKeys[i].GetExpression<TEntity>());
                }
            }
            return queryable;
        }

        /// <summary>
        /// 获取对象表达式指定属性的值
        /// 如获取:Out_Scheduling对象的ID或基他字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression">格式 Expression<Func<Out_Scheduling, object>>sch=x=>new {x.v1,x.v2} or x=>x.v1 解析里面的值返回为数组</param>
        /// <returns></returns>
        public static string[] GetExpressionToArray<TEntity>(this Expression<Func<TEntity, object>> expression)
        {
            string[] propertyNames = null;
            if (expression.Body is MemberExpression)
            {
                propertyNames = new string[] { ((MemberExpression)expression.Body).Member.Name };
            }
            else
            {
                propertyNames = expression.GetExpressionProperty().Distinct().ToArray();
            }
            return propertyNames;
        }

        /// <summary>
        /// 与下面and生成方式有所不同，如果直接用表达式1.2进行合并产会提示数据源不同的异常，只能使用下面的的and合并
        /// 此种合并是在使用的同一个数据源(变量),生成的sql语句同样有性能问题(本身可以索引扫描的,生成的sql语句的case when变成索引查找)
        /// <summary>
        /// 通过字段动态生成where and /or表达
        /// 如:有多个where条件，当条件成立时where 1=1 and/or 2=2,依次往后拼接
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listParas">ExpressionParameters
        /// 1、Field生成的字段
        /// 2、ExpressionType 表达式类型大于、小于、于大=、小于=、contains
        /// 3、Value表达式的值
        /// </param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(List<ExpressionParameters> listExpress)
        {
            return listExpress.Compose<T>(Expression.And);
        }
        /// <summary>
        /// 同上面and用法相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listExpress"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this List<ExpressionParameters> listExpress)
        {
            return listExpress.Compose<T>(Expression.Or);
        }
        private static Expression<Func<T, bool>> Compose<T>(this List<ExpressionParameters> listExpress, Func<Expression, Expression, Expression> merge)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
            Expression<Func<T, bool>> expression = null;
            foreach (ExpressionParameters exp in listExpress)
            {
                if (expression == null)
                {
                    expression = exp.Field.GetExpression<T, bool>(parameter);
                }
                else
                {
                    expression = expression.Compose(exp.Field.GetExpression<T, bool>(parameter), merge);
                }
            }
            return expression;
        }
        /// <summary>
        /// https://blogs.msdn.microsoft.com/meek/2008/05/02/linq-to-entities-combining-predicates/
        /// 表达式合并(合并生产的sql语句有性能问题)
        /// 合并两个where条件，如：多个查询条件时，判断条件成立才where
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)  
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with parameters from the first  
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression  
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static IQueryable<Result> GetQueryableSelect<Source, Result>(this IQueryable<Source> queryable)
        {
            Expression<Func<Source, Result>> expression = CreateMemberInitExpression<Source, Result>();
            return queryable.Select(expression);
        }

        /// <summary>
        /// 动态创建表达式Expression<Func<Animal, User>> expression = CreateMemberInitExpression<Animal, User>();
        ///结果为Expression<Func<Animal, User>> expression1 = x => new User() { Age = x.Age, Species = x.Species };
        ///参照文档https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.expressions.memberinitexpression?redirectedfrom=MSDN&view=netframework-4.8
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Result"></typeparam>
        /// <returns></returns>
        public static Expression<Func<Source, Result>> CreateMemberInitExpression<Source, Result>(Type resultType = null)
        {
            resultType = resultType ?? typeof(Result);
            ParameterExpression left = Expression.Parameter(typeof(Source), "p");
            NewExpression newExpression = Expression.New(resultType);
            PropertyInfo[] propertyInfos = resultType.GetProperties();
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                MemberExpression member = Expression.Property(left, propertyInfo.Name);
                MemberBinding speciesMemberBinding = Expression.Bind(resultType.GetMember(propertyInfo.Name)[0], member);
                memberBindings.Add(speciesMemberBinding);
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(newExpression, memberBindings);
            Expression<Func<Source, Result>> expression = Expression.Lambda<Func<Source, Result>>(memberInitExpression, new ParameterExpression[] { left });
            return expression;
        }
        public static Expression<Func<Source, object>> CreateMemberInitExpression<Source>(Type resultType)
        {
            return CreateMemberInitExpression<Source, object>(resultType);
        }
    }

    public class ExpressionParameters
    {
        public string Field { get; set; }
        public LinqExpressionType ExpressionType { get; set; }
        public object Value { get; set; }
        // public 
    }
    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }


    public enum QueryOrderBy
    {
        Desc = 1,
        Asc = 2
    }
}






public class LamadaExtention<Dto> where Dto : new()
{
    private List<Expression> m_lstExpression = null;
    private ParameterExpression m_Parameter = null;

    public LamadaExtention()
    {
        m_lstExpression = new List<Expression>();
        m_Parameter = Expression.Parameter(typeof(Dto), "x");
    }

    //构造表达式，存放到m_lstExpression集合里面
    public void GetExpression(string strPropertyName, object strValue, ExpressionType expressType)
    {
        Expression expRes = null;
        MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);

        //判断是否是时间类型 （这边出现传入的object 不是yyyy-MM-dd hh:mm:ss 类型的字符串可能会报错）
        if (member.Type == typeof(DateTime))
        {
            strValue = Convert.ToDateTime(strValue.ToString());
        }

        //if (expressType == ExpressionType.Contains)
        if (expressType == ExpressionType.Contains || expressType == ExpressionType.NotContains)
        {
            //expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
            Type proType = typeof(Dto).GetProperty(strPropertyName).PropertyType;
            ConstantExpression constant = proType.ToString() == "System.String"
            ? Expression.Constant(strValue) : Expression.Constant(strValue.ToString().ChangeType(proType));

            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            constant = Expression.Constant(strValue, typeof(string));
            if (expressType == ExpressionType.Contains)
            {
                //expRes = Expression.Lambda(Expression.Call(member, method, constant), null);
                expRes = Expression.Call(member, method, constant);
            }
            else
            {
                //expRes = Expression.Lambda(Expression.Not(Expression.Call(member, method, constant)), null);
                expRes = Expression.Call(member, method, constant);
            }

        }
        else if (expressType == ExpressionType.Equal)
        {
            expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
        }
        else if (expressType == ExpressionType.LessThan)
        {
            expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
        }
        else if (expressType == ExpressionType.LessThanOrEqual)
        {
            expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
        }
        else if (expressType == ExpressionType.GreaterThan)
        {
            expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
        }
        else if (expressType == ExpressionType.GreaterThanOrEqual)
        {
            expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
        }
        else if (expressType == ExpressionType.NotEqual)
        {
            expRes = Expression.NotEqual(member, Expression.Constant(strValue, member.Type));
        }
        //return expRes;
        m_lstExpression.Add(expRes);
    }

    //针对Or条件的表达式
    public void GetExpression(string strPropertyName, List<object> lstValue)
    {
        Expression expRes = null;
        MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
        foreach (var oValue in lstValue)
        {
            if (expRes == null)
            {
                expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
            }
            else
            {
                expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
            }
        }


        m_lstExpression.Add(expRes);
    }

    //得到Lamada表达式的Expression对象
    public Expression<Func<Dto, bool>> GetLambda()
    {
        Expression whereExpr = null;
        foreach (var expr in this.m_lstExpression)
        {
            if (whereExpr == null) whereExpr = expr;
            else whereExpr = Expression.And(whereExpr, expr);
        }
        if (whereExpr == null)
            return null;
        return Expression.Lambda<Func<Dto, Boolean>>(whereExpr, m_Parameter);
    }
}

//用于区分操作的枚举
public enum ExpressionType
{
    Contains=1,//like
    Equal=2,//等于
    LessThan=3,//小于
    LessThanOrEqual=4,//小于等于
    GreaterThan=5,//大于
    GreaterThanOrEqual=6,//大于等于
    NotEqual=7,
    NotContains = 8,
}
