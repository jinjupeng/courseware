using System;

namespace ApiServer.Model.Model
{
    public class Result<T> where T : class
    {

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
            this.code = resultCode.code();
            this.message = resultCode.message();
        }

        public Result(ResultCode resultCode, T data)
        {
            this.code = resultCode.code();
            this.message = resultCode.message();
            this.data = data;
        }

        public Result(String message)
        {
            this.message = message;
        }

        public static Result SUCCESS()
        {
            return new Result(ResultCode.SUCCESS);
        }

        public static <T> Result SUCCESS(T data)
        {
            return new Result(ResultCode.SUCCESS, data);
        }

        public static Result FAIL()
        {
            return new Result(ResultCode.FAIL);
        }

        public static Result FAIL(String message)
        {
            return new Result(message);
        }
    }
}
