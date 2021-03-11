using ApiServer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiServer.BLL.IBLL
{
    public interface IBaseService<T> where T : class
    {
        int AddRange(IEnumerable<T> t);
        int AddModel(T model);
        int DelRange(IEnumerable<T> t);
        int DelModel(T model);
        int DelBy(Expression<Func<T, bool>> exp);
        int ModifyRange(IEnumerable<T> t);
        int ModifyModel(T model);
        int CountAll(Expression<Func<T, bool>> where);

        T GetModel(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda);

        PageModel<T> QueryByPage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda);
        PageModel<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isDes = false);
    }
}