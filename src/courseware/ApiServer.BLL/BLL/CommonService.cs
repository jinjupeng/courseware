using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.Common;
using ApiServer.Model.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.BLL.BLL
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        public CommonService(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }

        public int GetUserId()
        {
            return JwtHelper.LoginUserId(_httpContextAccessor.HttpContext);
        }

        public UserDto GetUserDto()
        {
            return JwtHelper.LoginDto(_httpContextAccessor.HttpContext);
        }

        /// <summary>
        /// 微信加密数据解密
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="sessionId"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        public string WxDecrypt(string encryptedData, string sessionId, string vi)
        {
            var dict = (Dictionary<string, object>)_memoryCache.Get("WX_SESSION_ID" + sessionId);
            var sessionKey = dict["session_key"].ToString();
            return AESHelper.AESDecrypt(encryptedData, sessionKey, vi);
        }
    }
}
