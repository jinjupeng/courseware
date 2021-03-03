using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.Model.Model
{
    public class WXAuth
    {
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string sessionId { get; set; }
    }
}
