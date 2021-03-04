using System;

namespace ApiServer.Model.Model.MsgModel
{
    public partial class cwCourseware
    {

        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int? count { get; set; }
        public string url { get; set; }
        public DateTime? createTime { get; set; }
        public string cover { get; set; }
        public int? isCarousel { get; set; }
        public string carouselUrl { get; set; }

    }
}
