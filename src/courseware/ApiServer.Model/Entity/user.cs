using System;
using System.Collections.Generic;

namespace ApiServer.Model.Entity
{
    public partial class user
    {
        public user()
        {
            cw_exchange_key = new HashSet<cw_exchange_key>();
            cw_order = new HashSet<cw_order>();
            cw_user_courseware = new HashSet<cw_user_courseware>();
        }

        public int id { get; set; }
        public string nickname { get; set; }
        public string uuid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string portrait { get; set; }
        public string background { get; set; }
        public string phone_number { get; set; }

        public virtual ICollection<cw_exchange_key> cw_exchange_key { get; set; }
        public virtual ICollection<cw_order> cw_order { get; set; }
        public virtual ICollection<cw_user_courseware> cw_user_courseware { get; set; }
    }
}
