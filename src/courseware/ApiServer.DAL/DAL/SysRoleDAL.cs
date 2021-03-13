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
            var sql = $"select r.* from(select id 'user_id' from user where id = {userId}) t1 natural join sys_users_roles natural join sys_role r; ";
            var result = _baseDal.ExecSql(sql).ToList();
            return result;
        }
    }
}
