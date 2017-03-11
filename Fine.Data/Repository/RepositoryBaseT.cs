/*********************************************************************
 * Copyright © 2017 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using andyWqhCommon.Web;
using Fine.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fine.Data.Repository
{
    public class RepositoryBase<TEntity>: IRepositoryBaseT<TEntity> where TEntity:class,new ()
    {
        private AndFineDbContext dbContext = new AndFineDbContext();

        public int Insert(TEntity entity)
        {
            dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            return dbContext.SaveChanges();
        }
        public int Insert(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                dbContext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return dbContext.SaveChanges();
        }
        public int Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbContext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbContext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbContext.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return dbContext.SaveChanges();
        }
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = dbContext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbContext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbContext.SaveChanges();
        }
        public TEntity FindEntity(object keyValue)
        {
            return dbContext.Set<TEntity>().Find(keyValue);
        }
        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }
        public IQueryable<TEntity> IQueryable()
        {
            return dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }
        public List<TEntity> FindList(string strSql)
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql).ToList<TEntity>();
        }
        public List<TEntity> FindList(string strSql, DbParameter[] dbParameter)
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList<TEntity>();
        }
        public List<TEntity> FindList(Pagination pagination)
        {
            bool isAsc = pagination.Sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.Sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.Records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.Rows * (pagination.Page - 1)).Take<TEntity>(pagination.Rows).AsQueryable();
            return tempData.ToList();
        }
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            bool isAsc = pagination.Sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.Sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbContext.Set<TEntity>().Where(predicate);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.Records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.Rows * (pagination.Page - 1)).Take<TEntity>(pagination.Rows).AsQueryable();
            return tempData.ToList();
        }
    }
}
