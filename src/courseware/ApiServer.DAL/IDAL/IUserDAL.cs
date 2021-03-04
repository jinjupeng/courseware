using ApiServer.Model.Model.Dto;

namespace ApiServer.DAL.IDAL
{
    public interface IUserDAL
    {
        UserDto GetUserPermission(int userId);
        UserDto GetUserInfo(string uuid);
    }
}
