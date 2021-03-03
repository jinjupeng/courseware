using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class cw_user_courseware
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int cw_id { get; set; }
        public DateTime create_time { get; set; }

        public virtual cw_courseware cw_ { get; set; }
        public virtual user user_ { get; set; }
    }
}
