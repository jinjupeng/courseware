using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class cw_courseware
    {
        public cw_courseware()
        {
            cw_exchange_key = new HashSet<cw_exchange_key>();
            cw_order = new HashSet<cw_order>();
            cw_user_courseware = new HashSet<cw_user_courseware>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int? count { get; set; }
        public string url { get; set; }
        public DateTime? create_time { get; set; }
        public string cover { get; set; }
        public int? is_carousel { get; set; }
        public string carousel_url { get; set; }

        public virtual ICollection<cw_exchange_key> cw_exchange_key { get; set; }
        public virtual ICollection<cw_order> cw_order { get; set; }
        public virtual ICollection<cw_user_courseware> cw_user_courseware { get; set; }
    }
}
