using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiServer.DAL.DAL
{
    public class UserDAL : IUserDAL
    {
        public readonly BaseDal<user> _baseDal;
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly ContextMySql _context;
        public UserDAL(ContextMySql context, BaseDal<user> baseDal)
        {
            this._context = context;
        }

        public user GetUserInfo(string uuid)
        {
            return _baseDal.GetModels(a => a.uuid == uuid).SingleOrDefault();
        }

        public UserDto GetUserPermission(int userId)
        {
            var roles = _context.sys_users_roles;
            var result = from u in _context.sys_users_roles
                         join r in _context.sys_role_permission
                         on u.role_id equals r.role_id into x
                         where u.user_id = userId
                         select new UserDto
                         {

                         });
            return result;
        }
    }
}
