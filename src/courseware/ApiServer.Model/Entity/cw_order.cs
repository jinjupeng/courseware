using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class cw_order
    {
        public int id { get; set; }
        public string order_sn { get; set; }
        public int? cw_id { get; set; }
        public int user_id { get; set; }
        public decimal price { get; set; }
        public DateTime? create_time { get; set; }
        public DateTime? pay_time { get; set; }
        public bool? is_pay { get; set; }
        public int? pay_type { get; set; }
        public string wx_order { get; set; }

        public virtual cw_courseware cw_ { get; set; }
        public virtual sys_user user_ { get; set; }
    }
}
