using ApiServer.BLL.IBLL;
using ApiServer.Common;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class SysUserService : ISysUserService
    {
        private readonly IBaseService<sys_user> _baseService;

        public SysUserService(IBaseService<sys_user> baseService)
        {
            _baseService = baseService;
        }

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
            var userDto = new UserDto();
            var userModel = _baseService.GetModels(a => a.username == "起凡").FirstOrDefault();
            userDto = userModel.BuildAdapter().AdaptToType<sysUser>();
            return userDto;
        }

        public Result SignUp(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public UserDto UpdateInfo(sys_user user)
        {
            throw new System.NotImplementedException();
        }
    }
}
