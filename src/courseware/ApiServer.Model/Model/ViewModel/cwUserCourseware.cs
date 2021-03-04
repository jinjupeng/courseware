using System;

namespace ApiServer.Model.Model.MsgModel
{
    public partial class cwUserCourseware
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int cwId { get; set; }
        public DateTime createTime { get; set; }

    }
}
