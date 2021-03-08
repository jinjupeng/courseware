using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class sys_role
    {
        public int role_id { get; set; }
        public string name { get; set; }
        public int? level { get; set; }
        public string description { get; set; }
        public string data_scope { get; set; }
        public string create_by { get; set; }
        public string update_by { get; set; }
        public DateTime? create_time { get; set; }
        public DateTime? update_time { get; set; }
    }
}
