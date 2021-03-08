using ApiServer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiServer.BLL.IBLL
{
    public interface IBaseService<T> where T : class
    {
        bool AddRange(IEnumerable<T> t);
        bool AddRange(params T[] t);
        void DeleteRange(IEnumerable<T> t);
        void DeleteRange(params T[] t);
        void ModifyRange(IEnumerable<T> t);
        void ModifyRange(params T[] t);
        int CountAll(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据whereLambda获取IQueryable集合
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);

        PageModel<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy);
    }
}