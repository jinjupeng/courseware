using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.MsgModel;
using System.Collections.Generic;

namespace ApiServer.BLL.IBLL
{
    public interface ICwCoursewareService
    {

        bool AddCW(cwCourseware cwCourseware);
        bool UpdateCW(cw_courseware cwCourseware);
        bool DeleteCW(int id);
        cw_courseware GetCW(int id);
        PageModel<cw_courseware> listCW(int start);
        List<cw_courseware> GetCarousel();
    }
}
