using ApiServer.BLL.IBLL;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ApiServer.BLL.BLL
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseDal<T> _baseDal;

        public BaseService() { }
        public BaseService(IBaseDal<T> baseDal)
        {
            _baseDal = baseDal;
        }
        public int AddRange(IEnumerable<T> t)
        {
            _baseDal.AddRange(t);
            return _baseDal.Save();
        }

        public int AddModel(T model)
        {
            return _baseDal.AddAndSave(model);
        }

        public int DelRange(IEnumerable<T> t)
        {
            _baseDal.DelRange(t);
            return _baseDal.Save();
        }

        public int DelModel(T model)
        {
            return _baseDal.DelAndSave(model);
        }

        public int DelBy(Expression<Func<T, bool>> exp)
        {
            return _baseDal.DelAndSaveBy(exp);
        }

        public int ModifyRange(IEnumerable<T> t)
        {
            _baseDal.ModifyRange(t);
            return _baseDal.Save();
        }

        public int ModifyModel(T model)
        {
            return _baseDal.ModifyAndSave(model);
        }


        public int CountAll(Expression<Func<T, bool>> where)
        {
            return _baseDal.GetCount(where);
        }

        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return _baseDal.GetList(whereLambda);
        }

        public T GetModel(Expression<Func<T, bool>> whereLambda)
        {
            return _baseDal.GetModel(whereLambda);
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public PageModel<T> QueryByPage(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda)
        {
            var pageModel = new PageModel<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Size = 0,
                Total = 0,
                List = new List<T>()
            };

            if (pageIndex == 0 || pageSize == 0)
            {
                var list = _baseDal.GetList(whereLambda).ToList();
                pageModel.List = list;
                pageModel.Total = list.Count;
            }
            else
            {
                var query = _baseDal.GetList(whereLambda);
                pageModel.Total = query.Count();
                pageModel.List = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            return pageModel;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDes"></param>
        /// <returns></returns>
        public PageModel<T> QueryByPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isDes = false)
        {
            PageModel<T> pageModel = new PageModel<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                List = new List<T>()
            };
            if (pageIndex == 0 || pageSize == 0)
            {
                if (orderBy != null)
                {
                    pageModel.List = _baseDal.GetList(whereLambda, orderBy, isDes).ToList();
                    pageModel.Total = pageModel.List.Count;
                }
                else
                {
                    pageModel.List = _baseDal.GetList(whereLambda).ToList();
                    pageModel.Total = pageModel.List.Count;
                }
            }
            else
            {
                IQueryable<T> query;
                if (orderBy != null)
                {
                    query = _baseDal.GetList(whereLambda, orderBy, isDes);
                    pageModel.Total = query.Count();
                    pageModel.List = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    query = _baseDal.GetList(whereLambda);
                    pageModel.Total = query.Count();
                    pageModel.List = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            return pageModel;
        }
    }
}