using System;

namespace ApiServer.Model.Model.MsgModel
{
    public partial class sysRole
    {
        public int roleId { get; set; }
        public string name { get; set; }
        public int? level { get; set; }
        public string description { get; set; }
        public string dataScope { get; set; }
        public string createBy { get; set; }
        public string updateBy { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? updateTime { get; set; }
    }
}
