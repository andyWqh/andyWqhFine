/*********************************************************************
 * Copyright © 2017 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using Fine.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using andyWqhCommon.Web;
using System.Text.RegularExpressions;

namespace Fine.Data.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private AndFineDbContext dbContext = new AndFineDbContext();

        public DbTransaction dbTransaction { get; set; }

        public IRepositoryBase BeginTrans()
        {
            DbConnection dbConn = ((IObjectContextAdapter)dbContext).ObjectContext.Connection;
            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }
            //开始启动事物
            dbTransaction = dbConn.BeginTransaction();
            //返回当前调用者对象
            return this;
        }

        /// <summary>
        /// 事物提交
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            try
            {
                //保持变更到数据库中
                var returnValue = dbContext.SaveChanges();
                if (dbTransaction != null)
                {
                    //保存成功后及时提交事物
                    dbTransaction.Commit();
                }
                return returnValue;
            }
            catch (Exception)
            {
                if (dbTransaction != null)
                {
                    //操作异常时执行回滚操作保持数据一致性
                    this.dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                //释放资源
                this.Dispose();
            }
        }

        /// <summary>
        /// 回收释放连接资源
        /// </summary>
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.dbContext.Dispose();
        }

        public int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Added;
            return this.dbTransaction == null ? this.Commit() : 0;
        }

        public int Insert<TEntity>(List<TEntity> entityList) where TEntity : class
        {
            foreach (var entity in entityList)
            {
                dbContext.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Added;
            }
            return this.dbTransaction == null ? this.Commit() : 0;
        }

        public int Update<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties()
;
            foreach (PropertyInfo item in props)
            {
                if (item.GetValue(entity, null) != null)
                {
                    if(item.GetValue(entity,null).ToString() =="&nbsp;")
                    {
                        dbContext.Entry(entity).Property(item.Name).CurrentValue = null;
                        dbContext.Entry(entity).Property(item.Name).IsModified = true;
                    }
                }
            }
            return this.dbTransaction == null ? this.Commit() : 0;
        }

        public int Delete<TEntity>(TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Deleted;
            return this.dbTransaction == null ? this.Commit() : 0;
        }


        public int  Delete<TEntity>(Expression<Func<TEntity,bool>> predicate) where TEntity:class
        {
            var entitys = dbContext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbContext.Entry<TEntity>(m).State = System.Data.Entity.EntityState.Deleted);
            return this.dbTransaction == null ? this.Commit() : 0;
        }

        public TEntity FindEntity<TEntity>(object keyVaue) where TEntity : class
        {
            return dbContext.Set<TEntity>().Find(keyVaue);
        }

        public TEntity FindEntity<TEntity>(Expression<Func<TEntity,bool>>predicate) where TEntity:class
        {
            return dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class
        {
            return dbContext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbContext.Set<TEntity>().Where(predicate);
        }

        public List<TEntity>FindList<TEntity>(string strSql) where TEntity:class
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql).ToList<TEntity>();
        }

        public List<TEntity>FindList<TEntity>(string strSql,DbParameter[] dbParameter) where TEntity:class
        {
            return dbContext.Database.SqlQuery<TEntity>(strSql, dbParameter).ToList<TEntity>();
        }

        public List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new()
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

        public List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new()
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
