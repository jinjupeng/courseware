using ApiServer.BLL.IBLL;
using ApiServer.BLL.JWT;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.MsgModel;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class CwCoursewareService : ICwCoursewareService
    {
        private readonly IBaseDal<cw_courseware> _baseDAL;
        private readonly IBaseDal<cw_user_courseware> _cwUserCoursewareDal;
        private readonly ICommonService _commonService;
        private readonly IBaseService<cw_courseware> _baseService;

        public CwCoursewareService(IBaseDal<cw_courseware> baseDAL, IBaseDal<cw_user_courseware> cwUserCoursewareDal,
            ICommonService commonService, IBaseService<cw_courseware> baseService)
        {
            _baseDAL = baseDAL;
            _cwUserCoursewareDal = cwUserCoursewareDal;
            _commonService = commonService;
            _baseService = baseService;
        }

        public bool AddCW(cw_courseware cwcourseware)
        {
            return _baseDAL.AddAndSave(cwcourseware) > 0;
        }

        public bool DeleteCW(int id)
        {
            return _baseDAL.DelAndSaveBy(a => a.id == id) > 0;
        }

        public bool UpdateCW(cw_courseware cwcourseware)
        {
            return _baseDAL.ModifyAndSave(cwcourseware) > 0;
        }

        public List<cw_courseware> GetCarousel()
        {
            return _baseDAL.GetList(a => (int)a.is_carousel == 0, _ => _.is_carousel).ToList();
        }

        public cw_courseware GetCW(int id)
        {
            var userId = _commonService.GetUserId();
            var cwUserCoursewares = _cwUserCoursewareDal.GetList(a => a.user_id == userId).ToList();
            var isTrue = cwUserCoursewares.Any(a => a.cw_id == id);
            if (isTrue)
            {
                return _baseDAL.GetModel(a => a.id == id);
            }
            return null;
        }

        public PageModel<cw_courseware> ListCW(int pageIndex, int pageSize)
        {
            return _baseService.QueryByPage(pageIndex, pageSize, _ => _.is_carousel == 0, _ => _.is_carousel, false);
        }

    }
}
