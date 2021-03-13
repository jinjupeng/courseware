using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.Common;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;
using ApiServer.Model.Model.MsgModel;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class SysUserService : ISysUserService
    {
        private readonly IBaseService<sys_user> _baseService;
        private readonly IBaseDal<sys_user> _baseDal;
        private readonly ICommonService _commonService;
        private readonly ISysUserDal _sysUserDal;
        private readonly ISysRoleDal _sysRoleDal;
        private readonly IMemoryCache _memoryCache;
        private readonly string appid = ConfigTool.Configuration["wxmini:appid"];
        private readonly string secret = ConfigTool.Configuration["wxmini:secret"];

        public SysUserService(IBaseService<sys_user> baseService, ICommonService commonService,
            ISysUserDal sysUserDal, IMemoryCache memoryCache, IBaseDal<sys_user> baseDal, ISysRoleDal sysRoleDal)
        {
            _baseService = baseService;
            _commonService = commonService;
            _sysUserDal = sysUserDal;
            _memoryCache = memoryCache;
            _baseDal = baseDal;
            _sysRoleDal = sysRoleDal;
        }

        public Result AuthLogin(WXAuth wxAuth)
        {
            var wxDecrypt = _commonService.WxDecrypt(wxAuth.encryptedData, wxAuth.sessionId, wxAuth.iv);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(wxDecrypt);
            var phoneNumber = dict["phoneNumber"].ToString();
            var user = _baseDal.GetModel(a => a.phone_number == phoneNumber);
            if (user != null) // 登录
            {
                user.username = null;
                var userDto = new UserDto();
                userDto = user.BuildAdapter().AdaptToType<UserDto>();
                return Result.SUCCESS(Login(userDto));

            } // 注册
            else
            {
                var initPassword = CommonUtils.GetStringRandom(10);
                var userDto = new UserDto();
                userDto.phoneNumber = phoneNumber;
                userDto.password = initPassword;
                return SignUp(userDto);
            }
        }

        public Result DeleteUser(string uuid)
        {
            throw new System.NotImplementedException();
        }

        public string GetSessionId(string code)
        {
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appid}&secret={secret}&js_code={code}&grant_type=authorization_code";
            // 发送get请求
            var res = HttpUtil.HttpGet(url);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(res);
            var uuid = Guid.NewGuid().ToString();
            // uuid作为键将值dict放入到缓存中
            _memoryCache.Set("WX_SESSION_ID" + uuid, dict);
            return uuid;
        }

        public UserDto GetUserInfo(string uuid, bool refresh)
        {
            var userDto = _commonService.GetUserDto();
            var user = _baseService.GetModel(a => a.id == userDto.id);
            // 如果是自己获取自己的信息且刷新token
            if (user.uuid == uuid && refresh)
            {
                return Login(userDto);
            }

            // 如果是自己获取自己的信息
            else if (user.uuid == uuid)
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
            if (userModel == null)
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

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result SignUp(UserDto user)
        {
            var dict = new Dictionary<string, object>();
            var uuid = Guid.NewGuid().ToString();
            user.uuid = uuid;
            var stringRandom = CommonUtils.GetStringRandom(10);
            user.username = stringRandom;
            user.nickname = stringRandom;
            if (user.phoneNumber != null)
            {
                var queryUser = _baseDal.GetModel(a => a.phone_number == user.phoneNumber);
                if (queryUser == null)
                {
                    var sysUser = new sys_user();
                    sysUser = user.BuildAdapter().AdaptToType<sys_user>();
                    _baseDal.AddAndSave(sysUser);
                    var playLoad = new Dictionary<string, object>
                    {
                        { "uid", queryUser.id },
                        { "uname", queryUser.username },
                        { "role", "接单者" }
                    };

                    var token = JwtHelper.IssueJwt(playLoad);
                    dict.Add("token", token);
                    return Result.SUCCESS(dict);
                }
                else
                {
                    var userDto = new UserDto();
                    userDto = queryUser.BuildAdapter().AdaptToType<UserDto>();
                    // 用户存在直接登录
                    return Result.SUCCESS(Login(userDto));
                }
            }
            else
            {
                return new Result(ResultCode.PARAM_TYPE_BIND_ERROR);
            }
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
