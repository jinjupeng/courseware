using ApiServer.Model.Model.Dto;
using System.Collections.Generic;

namespace ApiServer.BLL.IBLL
{
    public interface ICwUserCoursewareService
    {

        List<CwUserCoursewareDto> GetMyCW();
    }
}
