using System;

namespace ApiServer.Model.Entity
{
    public partial class cw_exchange_key
    {
        public int id { get; set; }
        public string ex_key { get; set; }
        public int cw_id { get; set; }
        public bool? is_used { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? use_time { get; set; }
        public int? user_id { get; set; }

        public virtual cw_courseware cw_ { get; set; }
        public virtual sys_user user_ { get; set; }
    }
}
