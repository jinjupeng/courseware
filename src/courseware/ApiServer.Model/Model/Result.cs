using ApiServer.Common;
using System.Collections.Generic;
using System.Linq;

namespace ApiServer.Model.Model
{
    public class Result<T> where T : class
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
        public T data { get; set; }

        public Result()
        {
        }

        public Result(ResultCode resultCode)
        {
            this.code = int.Parse(resultCode.ToString());
            this.message = resultCodeList.Single(a => a.EnumValue == this.code).EnumName;
        }

        public Result(ResultCode resultCode, T data)
        {
            this.code = int.Parse(resultCode.ToString());
            this.message = resultCodeList.Single(a => a.EnumValue == this.code).EnumName;
            this.data = data;
        }

        public Result(string message)
        {
            this.message = message;
        }

        public static Result<T> SUCCESS()
        {
            return new Result<T>(ResultCode.SUCCESS);
        }

        public static Result<T> SUCCESS(T data)
        {
            return new Result<T>(ResultCode.SUCCESS, data);
        }

        public static Result<T> FAIL()
        {
            return new Result<T>(ResultCode.FAIL);
        }

        public static Result<T> FAIL(string message)
        {
            return new Result<T>(message);
        }
    }
}
