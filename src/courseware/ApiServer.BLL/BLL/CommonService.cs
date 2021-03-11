using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.Model.Model.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.BLL.BLL
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            return JwtHelper.LoginUserId(_httpContextAccessor.HttpContext);
        }

        public UserDto GetUserDto()
        {
            return JwtHelper.LoginDto(_httpContextAccessor.HttpContext);
        }
    }
}
