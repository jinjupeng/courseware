using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;

namespace ApiServer.BLL.BLL
{
    public class UserService : IUserService
    {
        public Result AuthLogin(WXAuth wxAuth)
        {
            throw new System.NotImplementedException();
        }

        public Result DeleteUser(string uuid)
        {
            throw new System.NotImplementedException();
        }

        public string GetSessionId(string code)
        {
            throw new System.NotImplementedException();
        }

        public UserDto GetUserInfo(string uuid, bool refresh)
        {
            throw new System.NotImplementedException();
        }

        public bool InitUserInfo(UserDto userDto)
        {
            throw new System.NotImplementedException();
        }

        public UserDto Login(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public Result SignUp(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public UserDto UpdateInfo(user user)
        {
            throw new System.NotImplementedException();
        }
    }
}
