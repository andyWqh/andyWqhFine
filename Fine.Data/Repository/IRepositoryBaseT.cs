using andyWqhCommon.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Data.Repository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryBaseT<TEntity> where  TEntity: class,new()
    {
        int Insert(TEntity entity);

        int Update(TEntity entity);

        int Delete(TEntity entity);

        int Delete(Expression<Func<TEntity, bool>> predicate);

        TEntity FindEntity(object keyValue);

        TEntity FindEntity(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> IQueryable();

        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> FindList(string strSql);

        List<TEntity> FindList(Pagination pagination);

        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
    }
}
