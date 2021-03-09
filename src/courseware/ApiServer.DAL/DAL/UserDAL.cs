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
        public readonly BaseDal<sys_user> _baseDal;
        /// <summary>
        /// EF上下文对象
        /// </summary>
        private readonly ContextMySql _context;
        public UserDAL(ContextMySql context, BaseDal<sys_user> baseDal)
        {
            this._context = context;
        }

        public sys_user GetUserInfo(string uuid)
        {
            return _baseDal.GetModels(a => a.uuid == uuid).SingleOrDefault();
        }

        public UserDto GetUserPermission(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
