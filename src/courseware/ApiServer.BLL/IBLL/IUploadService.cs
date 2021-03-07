using Microsoft.AspNetCore.Http;

namespace ApiServer.BLL.IBLL
{
    public interface IUploadService
    {
        string Upload(IFormCollection multipartFile);

        string UploadByPath(IFormCollection multipartFile, string path);

        int Delete(string url);
    }
}
