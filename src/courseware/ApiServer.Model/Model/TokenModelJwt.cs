
namespace ApiServer.Model.Model
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Sid
        /// </summary>
        public long Sid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

    }
}
