using ApiServer.BLL.IBLL;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using Mapster;
using System.Collections.Generic;

namespace ApiServer.BLL.BLL
{
    public class CwUserCoursewareService : ICwUserCoursewareService
    {
        private readonly IBaseDal<cw_user_courseware> _baseDal;

        public CwUserCoursewareService(IBaseDal<cw_user_courseware> baseDal)
        {
            _baseDal = baseDal;
        }

        public List<CwUserCoursewareDto> GetMyCW()
        {
            var userDto = new UserDto();
            var resultList = new List<CwUserCoursewareDto>();
            var list = _baseDal.GetList(a => a.user_id == userDto.id);
            resultList = list.BuildAdapter().AdaptToType<List<CwUserCoursewareDto>>();
            return resultList;
        }
    }
}
