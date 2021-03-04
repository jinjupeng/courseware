using ApiServer.DAL.IDAL;
using ApiServer.Model.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.DAL.DAL
{
    class UserDAL : IUserDAL
    {
        public UserDto GetUserInfo(string uuid)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserPermission(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
