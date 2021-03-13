using ApiServer.Model.Entity;
using System.Collections.Generic;

namespace ApiServer.DAL.IDAL
{
    public interface ISysRoleDal
    {
        List<sys_role> ListUserRoles(int userId);
    }
}
