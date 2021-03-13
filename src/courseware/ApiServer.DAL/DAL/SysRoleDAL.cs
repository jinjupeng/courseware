using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.DAL.DAL
{
    public class SysRoleDal : ISysRoleDal
    {
        public readonly IBaseDal<sys_role> _baseDal;

        public SysRoleDal(IBaseDal<sys_role> baseDal)
        {
            _baseDal = baseDal;
        }

        public List<sys_role> ListUserRoles(int userId)
        {
            var sql = $"SELECT r.* FROM(SELECT id 'user_id' FROM sys_user WHERE id = {userId}) t1 NATURAL JOIN sys_users_roles NATURAL JOIN sys_role r; ";
            var result = _baseDal.ExecSql(sql).ToList();
            return result;
        }
    }
}
