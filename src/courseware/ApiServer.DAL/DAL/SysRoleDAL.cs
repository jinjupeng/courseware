using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiServer.DAL.DAL
{
    public class SysRoleDAL : ISysRoleDAL
    {
        public readonly BaseDal<sys_role> _baseDal;
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly ContextMySql _context;
        public SysRoleDAL(ContextMySql context, BaseDal<sys_role> baseDal)
        {
            this._context = context;
            _baseDal = baseDal;
        }

        public List<sys_role> ListUserRoles(int userId)
        {
            var sql = $"select r.* from(select id 'user_id' from user where id = {userId}) t1 natural join sys_users_roles natural join sys_role r; ";
            var result = _baseDal.ExecSql(sql).ToList();
            return result;
        }
    }
}
