using ApiServer.BLL.IBLL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _sysUserService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserService"></param>
        public SysUserController(ISysUserService sysUserService)
        {
            _sysUserService = sysUserService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            var result = _sysUserService.Login(user);
            // TODO：生成token
            user.password = "";
            user.token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIyIiwiZmlkIjoiMSIsInVuIjoiemhhbmdzYW4iLCJzdXAiOiJGYWxzZSIsIm5iZiI6IjE2MTUxOTkwMTEiLCJleHAiOjE2MTc3MTkwMTAsImlzcyI6Imx0LmFtaXM4LjAiLCJhdWQiOiJsdC5hbWlzOC4wIn0.08EeihVX-j7Fs5n0Nz7qepO7Tk0fdcHxr65PfJtP9ng";
            return Ok(await Task.FromResult(Result.SUCCESS(result)));
        }

        [HttpPost]
        [Route("signUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserDto user)
        {
            var result = _sysUserService.SignUp(user);
            return Ok(await Task.FromResult(result));
        }

        [HttpGet]
        [Route("userinfo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfo([FromQuery] string uuid, bool refresh = false)
        {
            var result = _sysUserService.GetUserInfo(uuid, refresh);
            return Ok(await Task.FromResult(Result.SUCCESS(result)));
        }

        [HttpPost]
        [Route("updateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser([FromBody] sys_user user)
        {
            var result = _sysUserService.UpdateInfo(user);
            //todo
            return Ok(await Task.FromResult(Result.SUCCESS(result)));
        }

        [HttpPost]
        [Route("authLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthLogin([FromBody] WXAuth wxAuth)
        {
            var result = _sysUserService.AuthLogin(wxAuth);
            return Ok(await Task.FromResult(Result.SUCCESS(result)));
        }

        [HttpGet]
        [Route("getSessionId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSessionId([FromQuery] string code)
        {
            var sessionId = _sysUserService.GetSessionId(code);
            var dict = new Dictionary<string, string>
            {
                { "sessionId", sessionId }
            };
            return Ok(await Task.FromResult(Result.SUCCESS(dict)));
        }

        [HttpPost]
        [Route("initUserInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> InitUserInfo([FromBody] UserDto user)
        {
            var result = _sysUserService.InitUserInfo(user);
            return Ok(await Task.FromResult(Result.SUCCESS(result)));
        }
    }
}
