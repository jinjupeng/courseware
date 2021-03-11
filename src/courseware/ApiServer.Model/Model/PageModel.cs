using System.Collections.Generic;

namespace ApiServer.Model.Model
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; } = 0;

        /// <summary>
        /// 当前页的数量
        /// </summary>
        public int Size { set; get; }

        /// <summary>
        /// 结果集
        /// </summary>
        public List<T> List { get; set; }

    }
}
