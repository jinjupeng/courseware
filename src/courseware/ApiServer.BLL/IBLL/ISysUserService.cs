using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;

namespace ApiServer.BLL.IBLL
{
    public interface ISysUserService
    {

        UserDto Login(UserDto user);

        Result SignUp(UserDto user);

        Result AuthLogin(WXAuth wxAuth);

        UserDto GetUserInfo(string uuid, bool refresh);
        bool InitUserInfo(UserDto userDto);
        string GetSessionId(string code);

        Result DeleteUser(string uuid);

        UserDto UpdateInfo(sys_user user);
    }
}
