using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ApiServer.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 枚举转list集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumEntity> EnumToList<T>()
        {
            var list = new List<EnumEntity>();
            foreach (var e in System.Enum.GetValues(typeof(T)))
            {
                var m = new EnumEntity();
                var objArr = e.GetType().GetField(e.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr.Any() && objArr.Length > 0)
                {
                    if (objArr[0] is DescriptionAttribute da) m.Description = da.Description;
                }

                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }

            return list;
        }
    }

    public class EnumEntity
    {
        /// <summary>
        /// 枚举的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string EnumName { get; set; }

        /// <summary>
        /// 枚举对象的值
        /// </summary>
        public int EnumValue { get; set; }
    }
}
