using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;

namespace ApiServer.DAL.IDAL
{
    public interface IUserDAL
    {
        UserDto GetUserPermission(int userId);
        sys_user GetUserInfo(string uuid);
    }
}
