using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServer.Model.Model
{
    public class QueryResult<T>
    {
        //数据列表
        private List<T> list;

        //数据总数
        private long total;

        public QueryResult(List<T> list, long total)
        {
            this.list = list;
            this.total = total;
        }

        public QueryResult() { }

        public List<T> GetList()
        {
            return list;
        }

        public void SetList(List<T> list)
        {
            this.list = list;
        }

        public long GetTotal()
        {
            return total;
        }

        public void SetTotal(long total)
        {
            this.total = total;
        }
    }
}
