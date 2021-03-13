using ApiServer.Model.Model.Dto;

namespace ApiServer.BLL.IBLL
{
    public interface ICommonService
    {
        // 获取请求ip

        // 获取当前登录人id
        int GetUserId();

        // 获取当前userDto
        UserDto GetUserDto();

        string WxDecrypt(string encryptedData, string sessionId, string vi);
    }
}
