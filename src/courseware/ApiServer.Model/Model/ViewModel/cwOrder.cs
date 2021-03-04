using System;

namespace ApiServer.Model.Model.MsgModel
{
    public partial class cwOrder
    {
        public int id { get; set; }
        public string orderSn { get; set; }
        public int? cwId { get; set; }
        public int userId { get; set; }
        public decimal price { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? payTime { get; set; }
        public bool? isPay { get; set; }
        public int? payType { get; set; }
        public string wxOrder { get; set; }

    }
}
