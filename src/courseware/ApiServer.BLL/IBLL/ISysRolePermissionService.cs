namespace ApiServer.BLL.IBLL
{
    public interface ISysRolePermissionService
    {

        string upload(MultipartFile multipartFile);

        string uploadByPath(MultipartFile multipartFile, string path);

        int delete(string url);
    }
}
