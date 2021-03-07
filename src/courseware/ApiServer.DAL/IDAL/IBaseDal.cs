using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiServer.DAL.IDAL
{
    public interface IBaseDal<T> where T : class
    {
        void Add(T model);
        Task AddAsync(T model);
        void AddRange(IEnumerable<T> model);
        Task AddRangeAsync(IEnumerable<T> model);

        int AddAndSave(T model);
        Task<int> AddAndSaveAsync(T model);
        int AddRangeAndSave(IEnumerable<T> model);
        Task<int> AddRangeAndSaveAsync(IEnumerable<T> model);

        void Del(T model);
        void DelRange(IEnumerable<T> model);

        int DelAndSave(T model);
        Task<int> DelAndSaveAsync(T model);
        int DelRangeAndSave(IEnumerable<T> model);
        Task<int> DelRangeAndSaveAsync(IEnumerable<T> model);
        void DelBy(Expression<Func<T, bool>> delWhere);
        Task DelByAsync(Expression<Func<T, bool>> delWhere);

        int DelAndSaveBy(Expression<Func<T, bool>> delWhere);
        Task<int> DelAndSaveByAsync(Expression<Func<T, bool>> delWhere);

        void Modify(T model);
        void ModifyRange(IEnumerable<T> model);
        int ModifyAndSave(T model);
        Task<int> ModifyAndSaveAsync(T model);
        int ModifyRangeAndSave(IEnumerable<T> model);
        Task<int> ModifyRangeAndSaveAsync(IEnumerable<T> model);

        T GetModel(Expression<Func<T, bool>> where);
        Task<T> GetModelAsync(Expression<Func<T, bool>> where);
        Task<T> GetModelAsync<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isDes = false);
        IQueryable<T> GetIQueryable(Expression<Func<T, bool>> where);

        int GetCount(Expression<Func<T, bool>> where);
        Task<int> GetCountAsync(Expression<Func<T, bool>> where);

        int Save();
        Task<int> SaveAsync();

        IQueryable<T> ExecSql(string sql);

        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy);

    }
}