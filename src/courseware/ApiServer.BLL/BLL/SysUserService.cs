using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.Common;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;
using ApiServer.Model.Model.MsgModel;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class SysUserService : ISysUserService
    {
        private readonly IBaseService<sys_user> _baseService;
        private readonly ICommonService _commonService;
        private readonly ISysUserDal _sysUserDal;
        private readonly ISysRoleDal _sysRoleDal;
        private readonly string appid = ConfigTool.Configuration["wxmini:appid"];
        private readonly string secret = ConfigTool.Configuration["wxmini:secret"];

        public SysUserService(IBaseService<sys_user> baseService, ICommonService commonService,
            ISysUserDal sysUserDal)
        {
            _baseService = baseService;
            _commonService = commonService;
            _sysUserDal = sysUserDal;
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
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
            url = url.Replace("{0}", appid).Replace("{1}", secret).Replace("{2}", code);
            // todo
            return url;
        }

        public UserDto GetUserInfo(string uuid, bool refresh)
        {
            var userDto = _commonService.GetUserDto();
            var user = _baseService.GetModel(a => a.id == userDto.id);
            // 如果是自己获取自己的信息且刷新token
            if(user.uuid == uuid && refresh)
            {
                return Login(userDto);
            }

            // 如果是自己获取自己的信息
            else if(user.uuid == uuid)
            {
                userDto.password = "";
                return userDto;
            }

            // 如果是获取他人的信息
            else
            {
                var userInfo = new UserDto();
                var sysUser = _sysUserDal.GetUserInfo(uuid);
                userInfo = sysUser.BuildAdapter().AdaptToType<UserDto>();
                return userInfo;
            }
        }

        public bool InitUserInfo(UserDto userDto)
        {
            userDto.password = "";
            userDto.username = "";
            var sysUser = userDto.BuildAdapter().AdaptToType<sys_user>();
            return _baseService.ModifyModel(sysUser) > 0;
        }

        public UserDto Login(UserDto user)
        {
            var userDto = new UserDto();
            var userModel = _baseService.GetModel(a => a.username == user.username && a.password == user.password);
            if(userDto == null)
            {
                throw new CustomException(500, "用户名或密码错误！");
            }
            var roleName = _sysRoleDal.ListUserRoles(userModel.id).FirstOrDefault()?.name;
            var playLoad = new Dictionary<string, object>
            {
                { "uid", userModel.id },
                { "uname", userModel.username },
                { "role", roleName } // 这里应该需要从数据库中读取
            };
            var jwt = JwtHelper.IssueJwt(playLoad);
            userDto = userModel.BuildAdapter().AdaptToType<UserDto>();
            userDto.token = jwt;
            userDto.password = "";
            return userDto;
        }

        public Result SignUp(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public UserDto UpdateInfo(sys_user user)
        {
            var dto = new UserDto();
            var userDto = _commonService.GetUserDto();
            user.id = userDto.id;
            var result = _baseService.ModifyModel(user) > 0;
            if (result)
            {
                var sysUser = _baseService.GetModel(a => a.id == user.id);
                dto = sysUser.BuildAdapter().AdaptToType<UserDto>();
                dto.password = "";
                dto.phoneNumber = "";
            }
            return dto;
        }
    }
}
