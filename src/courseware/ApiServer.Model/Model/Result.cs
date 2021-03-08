using ApiServer.Common;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.Model.Model
{
    public class Result
    {
        private readonly List<EnumEntity> resultCodeList = EnumHelper.EnumToList<ResultCode>();

        /// <summary>
        /// 操作代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public object data { get; set; }

        public Result()
        {
        }

        public Result(ResultCode resultCode)
        {
            this.code = resultCodeList.Single(a => a.EnumName == resultCode.ToString()).EnumValue;
            this.message = resultCodeList.Single(a => a.EnumName == resultCode.ToString()).Description;
        }

        public Result(ResultCode resultCode, object data)
        {
            this.code = resultCodeList.Single(a => a.EnumName == resultCode.ToString()).EnumValue;
            this.message = resultCodeList.Single(a => a.EnumName == resultCode.ToString()).Description;
            this.data = data;
        }

        public Result(string message)
        {
            this.message = message;
        }

        public static Result SUCCESS()
        {
            return new Result(ResultCode.SUCCESS);
        }

        public static Result SUCCESS(object data)
        {
            return new Result(ResultCode.SUCCESS, data);
        }

        public static Result FAIL()
        {
            return new Result(ResultCode.FAIL);
        }

        public static Result FAIL(string message)
        {
            return new Result(message);
        }
    }
}
