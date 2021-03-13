using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using System.Collections.Generic;

namespace ApiServer.BLL.IBLL
{
    public interface ICwCoursewareService
    {

        bool AddCW(cw_courseware cwCourseware);
        bool UpdateCW(cw_courseware cwCourseware);
        bool DeleteCW(int id);
        cw_courseware GetCW(int id);
        PageModel<cw_courseware> ListCW(int pageIndex, int pageSize);
        List<cw_courseware> GetCarousel();
    }
}
