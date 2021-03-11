using ApiServer.Model.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.BLL.IBLL
{
    public interface ICommonService
    {
        // 获取请求ip

        // 获取当前登录人id
        int GetUserId();

        // 获取当前userDto
        UserDto GetUserDto();
    }
}
