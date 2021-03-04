using ApiServer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.DAL.IDAL
{
    public interface ISysRoleDAL
    {
        List<sys_role> ListUserRoles(int userId);
    }
}
