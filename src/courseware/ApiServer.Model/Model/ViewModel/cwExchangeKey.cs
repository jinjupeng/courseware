using System;

namespace ApiServer.Model.Model.MsgModel
{
    public partial class cwExchangeKey
    {
        public int id { get; set; }
        public string exKey { get; set; }
        public int cwId { get; set; }
        public bool? isUsed { get; set; }
        public DateTime createTime { get; set; }
        public DateTime? useTime { get; set; }
        public int? userId { get; set; }
    }
}
