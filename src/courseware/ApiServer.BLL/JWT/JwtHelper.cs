using ApiServer.Common;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiServer.BLL.JWT
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtHelper
    {
        private static JwtSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        public static JwtSettings Settings { set => _settings = value; }

        /// <summary>
        /// 秘钥，可以从配置文件中获取
        /// </summary>
        public static string SecurityKey = ConfigTool.Configuration["Jwt:SecurityKey"];

        /// <summary>
        /// 过期时间
        /// </summary>
        public static string ExpireMinutes = ConfigTool.Configuration["Jwt:ExpireMinutes"];

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="payLoad"></param>
        /// <returns></returns>
        public static string IssueJwt(Dictionary<string, object> payLoad)
        {
            // 这里就是声明我们的claim
            var claims = new Claim[] {
                // token添加自定义参数
                new Claim("uid", payLoad["uid"].ToString()),
                new Claim("uname", payLoad["uname"].ToString()),
                // 这个Role是官方UseAuthentication要要验证的Role，也就是Controller接口上的[Authorize(Roles = "管理员")]
                new Claim(ClaimTypes.Role, payLoad["role"].ToString()),

                //Claim的默认配置
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                // 这个就是过期时间，目前是过期600秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim (JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddSeconds(600)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _settings.Audience),
                new Claim(JwtRegisteredClaimNames.Sub, "dotnetore")

            };

            // 密钥(SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                // expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }


        /// <summary>
        /// 刷新token值
        /// </summary>
        /// <returns></returns>
        public static string RefreshToken(string token)
        {
            string tokenStr = token;
            var oldToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var oldClaims = oldToken.Claims;
            if (long.Parse(GetPayLoad(token)["exp"].ToString()) - ToUnixEpochDate(DateTime.UtcNow) <= 500)
            {
                // 这里就是声明我们的claim
                var claims = new List<Claim>(); // 从旧token中获取到Claim
                claims.AddRange(oldClaims.Where(t => t.Type != JwtRegisteredClaimNames.Iat));
                //重置token的发布时间为当前时间
                string nowDate = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                claims.Add(new Claim(JwtRegisteredClaimNames.Iat, nowDate, ClaimValueTypes.Integer64));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken
                (
                    issuer: _settings.Issuer,
                    audience: _settings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_settings.ExpireMinutes)),
                    signingCredentials: cred
                );
                tokenStr = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            return tokenStr;
        }

        /// <summary>
        /// 验证身份 验证签名的有效性,
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// 例如：payLoad["aud"]?.ToString() == "roberAuddience";
        /// 例如：验证是否过期 等
        /// <returns></returns>
        public static bool Validate(string encodeJwt)
        {
            //encodeJwt = encodeJwt.ToString().Substring("Bearer ".Length).Trim();
            var jwtArr = encodeJwt.Split('.');
            //var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));

            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(SecurityKey));

            var encodedSignature = Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1]))));

            //首先验证签名是否正确（必须的)
            bool success = string.Equals(jwtArr[2], encodedSignature);
            if (!success)
            {
                return false;
            }

            //其次验证是否在有效期内（也应该必须）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = now <= long.Parse(payLoad["exp"].ToString()) && now >= long.Parse(payLoad["nbf"].ToString());
            return success;
        }

        /// <summary>
        /// 获取jwt中的payload
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPayLoad(string encodeJwt)
        {
            var jwtArr = encodeJwt.Split('.');
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));
            return payLoad;
        }

        /// <summary>
        /// datetime转时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// 获取登录人id
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static int LoginUserId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            var payLoad = GetPayLoad(authStr);
            return int.Parse(payLoad["uid"].ToString());
        }

        /// <summary>
        /// 获取登录名
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string LoginUserName(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            var payLoad = GetPayLoad(authStr);
            return (payLoad["uname"].ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static UserDto LoginDto(HttpContext httpContext)
        {
            var result = new UserDto();
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            var payLoad = GetPayLoad(authStr);
            result.id = int.Parse(payLoad["uid"].ToString());
            result.username = payLoad["uname"].ToString();
            return result;

        }
    }
}
