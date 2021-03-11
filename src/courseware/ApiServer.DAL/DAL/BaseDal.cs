using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiServer.DAL.DAL
{
    public class BaseDal<T> : IBaseDal<T> where T : class
    {
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly ContextMySql _context;

        public BaseDal(ContextMySql context)
        {
            this._context = context;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="model">新增的实体</param>
        public void Add(T model)
        {
            _context.Set<T>().Add(model);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="model">新增的实体</param>
        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="model">新增的实体</param>
        public void AddRange(IEnumerable<T> model)
        {
            _context.Set<T>().AddRange(model);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="model">新增的实体</param>
        public async Task AddRangeAsync(IEnumerable<T> model)
        {
            await _context.Set<T>().AddRangeAsync(model);
        }
        
        /// <summary>
        /// 新增实体并保存
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <returns>受影响的行数</returns>
        public int AddAndSave(T model)
        {
            Add(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 新增实体并保存
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> AddAndSaveAsync(T model)
        {
            await AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 新增实体并保存
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <returns>受影响的行数</returns>
        public int AddRangeAndSave(IEnumerable<T> model)
        {
            AddRange(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 新增实体并保存
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> AddRangeAndSaveAsync(IEnumerable<T> model)
        {
            await AddRangeAsync(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model">删除的实体</param>
        public void Del(T model)
        {
            _context.Set<T>().Remove(model);
        }


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model">删除的实体</param>
        public void DelRange(IEnumerable<T> model)
        {
            _context.Set<T>().RemoveRange(model);
        }
        
        /// <summary>
        /// 删除实体并保存
        /// </summary>
        /// <param name="model">删除的实体</param>
        /// <returns>受影响的行数</returns>
        public int DelAndSave(T model)
        {
            Del(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 删除实体并保存
        /// </summary>
        /// <param name="model">删除的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> DelAndSaveAsync(T model)
        {
            Del(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除实体并保存
        /// </summary>
        /// <param name="model">删除的实体</param>
        /// <returns>受影响的行数</returns>
        public int DelRangeAndSave(IEnumerable<T> model)
        {
            DelRange(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 删除实体并保存
        /// </summary>
        /// <param name="model">删除的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> DelRangeAndSaveAsync(IEnumerable<T> model)
        {
            DelRange(model);
            return await _context.SaveChangesAsync();
        }


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="delWhere">条件</param>
        public void DelBy(Expression<Func<T, bool>> delWhere)
        {
            //查询要删除的数据
            List<T> listDeleting = _context.Set<T>().Where(delWhere).ToList();
            //将要删除的数据 用删除方法添加到 EF 容器中
            DelRange(listDeleting);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="delWhere">条件</param>
        public async Task DelByAsync(Expression<Func<T, bool>> delWhere)
        {
            //查询要删除的数据
            List<T> listDeleting = await _context.Set<T>().Where(delWhere).ToListAsync();
            //将要删除的数据 用删除方法添加到 EF 容器中
            DelRange(listDeleting);
        }

        /// <summary>
        /// 根据条件删除并保存
        /// </summary>
        /// <param name="delWhere">条件</param>
        public int DelAndSaveBy(Expression<Func<T, bool>> delWhere)
        {
            DelBy(delWhere);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 根据条件删除并保存
        /// </summary>
        /// <param name="delWhere">条件</param>
        public async Task<int> DelAndSaveByAsync(Expression<Func<T, bool>> delWhere)
        {
            await DelByAsync(delWhere);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model">修改的实体</param>
        public void Modify(T model)
        {
            _context.Set<T>().Update(model);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model">修改的实体</param>
        public void ModifyRange(IEnumerable<T> model)
        {
            _context.Set<T>().UpdateRange(model);
        }

        /// <summary>
        /// 修改实体并保存
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <returns>受影响的行数</returns>
        public int ModifyAndSave(T model)
        {
            Modify(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 修改实体并保存
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> ModifyAndSaveAsync(T model)
        {
            Modify(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 修改实体并保存
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <returns>受影响的行数</returns>
        public int ModifyRangeAndSave(IEnumerable<T> model)
        {
            ModifyRange(model);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 修改实体并保存
        /// </summary>
        /// <param name="model">修改的实体</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> ModifyRangeAndSaveAsync(IEnumerable<T> model)
        {
            ModifyRange(model);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 根据条件查询单个对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetModel(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(where);
        }

        /// <summary>
        /// 根据条件查询单个对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<T> GetModelAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(where);
        }

        /// <summary>
        /// 根据条件排序查询单个对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<T> GetModelAsync<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isDes = false)
        {
            var query = _context.Set<T>().AsNoTracking().Where(whereLambda);
            query = isDes ? query.OrderByDescending(orderLambda) : query.OrderBy(orderLambda);
            return await query.FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据条件查询集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> where)
        {
            var list = _context.Set<T>().AsNoTracking().Where(where);
            return list;
        }

        /// <summary>
        /// 根据条件查询排序集合
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">查询条件 lambda表达式</param>
        /// <param name="orderLambda">排序条件 lambda表达式</param>
        /// // <param name="isDes">是否降序</param>
        /// <returns>返回的集合</returns>
        public IQueryable<T> GetList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda, bool isDes = false)
        {
            var query = _context.Set<T>().AsNoTracking().Where(whereLambda);
            var list = isDes ? query.OrderByDescending(orderLambda) : query.OrderBy(orderLambda);
            return list;
        }

        /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().AsNoTracking().Count(where);
        }


        /// <summary>
        /// 根据条件查询个数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().AsNoTracking().CountAsync(where);
        }

        public IQueryable<T> ExecSql(string sql)
        {
            return _context.Set<T>().FromSqlRaw(sql).AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// 保存数据库
        /// </summary>
        /// <returns></returns>
        public int Save() => _context.SaveChanges();

        /// <summary>
        /// 保存数据库
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    }
}