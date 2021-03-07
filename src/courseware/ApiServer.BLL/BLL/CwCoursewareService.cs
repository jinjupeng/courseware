using ApiServer.BLL.IBLL;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model;
using ApiServer.Model.Model.MsgModel;
using System.Collections.Generic;

namespace ApiServer.BLL.BLL
{
    public class CwCoursewareService : ICwCoursewareService
    {
        private readonly IBaseDal<cw_courseware> _baseDAL;

        public CwCoursewareService(IBaseDal<cw_courseware> baseDAL)
        {
            _baseDAL = baseDAL;
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
            throw new System.NotImplementedException();
        }

        public cw_courseware GetCW(int id)
        {
            throw new System.NotImplementedException();
        }

        public PageModel<cw_courseware> listCW(int start)
        {
            throw new System.NotImplementedException();
        }

    }
}
