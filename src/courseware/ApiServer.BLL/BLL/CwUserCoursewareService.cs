using ApiServer.BLL.IBLL;
using ApiServer.DAL.IDAL;
using ApiServer.Model.Entity;
using ApiServer.Model.Model.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.BLL.BLL
{
    public class CwUserCoursewareService : ICwUserCoursewareService
    {
        private readonly ContextMySql _context;

        public CwUserCoursewareService(ContextMySql context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取我的课程
        /// </summary>
        /// <returns></returns>
        public List<CwUserCoursewareDto> GetMyCW(int id)
        {
            var cwUserCoursewares = _context.Set<cw_user_courseware>().AsNoTracking();
            var cwCoursewares = _context.Set<cw_courseware>().AsNoTracking();
            var list = (from cu in cwUserCoursewares
                       join cc in cwCoursewares on cu.cw_id equals cc.id
                       where cu.user_id == id
                       select new CwUserCoursewareDto
                       {
                          id = cu.id,
                          userId = cu.user_id,
                          createTime = cu.create_time,
                          courseware = cc
                       }).ToList();
            return list;
        }
    }
}
