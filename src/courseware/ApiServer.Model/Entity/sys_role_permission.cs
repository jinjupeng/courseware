using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class sys_role_permission
    {
        public int id { get; set; }
        public int role_id { get; set; }
        public string permission { get; set; }
    }
}
