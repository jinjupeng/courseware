using ApiServer.Model.Entity;
using System;

namespace ApiServer.Model.Model.Dto
{
    public class CwUserCoursewareDto
    {
        public int id { get; set; }

        public int userId { get; set; }
        public user user { get; set; }
        public cwCourseware courseware { get; set; }
        public int cwId { get; set; }

        public DateTime createTime { get; set; }
    }
}
