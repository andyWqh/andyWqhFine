/*********************************************************************
 * Copyright © 2017 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using andyWqhCommon.Web;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Data.Repository
{
    public interface IRepositoryBase : IDisposable
    {
        IRepositoryBase BeginTrans();

        int Commit();

        int Insert<TEntity>(TEntity entity) where TEntity : class;

        int Insert<TEntity>(List<TEntity> list) where TEntity : class;

        int Update<TEntity>(TEntity entity) where TEntity : class;

        int Delete<TEntity>(TEntity entity) where TEntity : class;

        int Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        TEntity FindEntity<TEntity>(object keyValue) where TEntity : class;

        TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class;

        IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        List<TEntity> FindList<TEntity>(string strSql) where TEntity : class;

        List<TEntity> FindList<TEntity>(string strSql, DbParameter[] pars) where TEntity : class;

        List<TEntity> FindList<TEntity>(Pagination pagination) where TEntity : class, new();

        List<TEntity> FindList<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class, new();
    }
}
