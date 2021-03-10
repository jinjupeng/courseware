using System.Collections.Generic;

namespace ApiServer.Model.Model.Dto
{
    public class UserDto
    {
        public int id { get; set; }
        public string nickname { get; set; }

        /// <summary>
        /// 更新的时候可以为null(代表不更新)
        /// </summary>
        public string username { get; set; }
        public string uuid { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public string background { get; set; }
        public string portrait { get; set; }

        #region dto拓展属性
        public string token { get; set; }
        public List<string> permissions { get; set; }
        public List<string> roles { get; set; }
        //验证码
        public string code { get; set; }

        #endregion
    }
}
