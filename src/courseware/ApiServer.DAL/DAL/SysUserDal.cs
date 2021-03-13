using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using System;

namespace ApiServer.DAL.DAL
{
    public class SysUserDal : ISysUserDal
    {
        public readonly IBaseDal<sys_user> _baseDal;

        public SysUserDal(IBaseDal<sys_user> baseDal)
        {
            this._baseDal = baseDal;
        }

        public sys_user GetUserInfo(string uuid)
        {
            return _baseDal.GetModel(a => a.uuid == uuid);
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserDto GetUserPermission(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
