/*********************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Extend
{
    /// <summary>
    /// 分部类 编译后合并dll
    /// </summary>
    public static partial class Ext
    {
        #region 数值转换

        /// <summary>
        /// 转换成整数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt(this object data)
        {
            if (data == null)
            {
                return default(int);
            }
            int result;
            var success = int.TryParse(data.ToString(), out result);
            if (success)
            {
                return result;
            }
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return default(int);
            }
        }

        /// <summary>
        /// 转换成空整数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
            {
                return default(int?);
            }
            int result;
            var isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
            {
                return result;
            }
            else
            {
                return default(int?);
            }
        }

        /// <summary>
        /// 转换成双精度浮点数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double ToDouble(this object data)
        {
            if (data == null)
            {
                return default(double);
            }
            double result;
            return double.TryParse(data.ToString(), out result) ? result : default(double);
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data),digits);
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == null)
                return 0;
            decimal result;
            return decimal.TryParse(data.ToString(), out result) ? result : 0;
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
            {
                return default(decimal?);
            }
            decimal result;
            var isValid = decimal.TryParse(data.ToString(), out result);
            return isValid ? result : default(decimal?);
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == null)
                return null;
            return Math.Round(result.Value, digits);
        }
        #endregion

        #region 日期转换

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object data)
        {
            if (data == null)
            {
                return  DateTime.MinValue;
            }
            DateTime result;
            return DateTime.TryParse(data.ToString(), out result)
                ? result
                : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateTimeOrNull(this object data)
        {
            if (data == null)
            {
                return null;
            }
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }
        #endregion

        #region 布尔转换
        ///<summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == null)
            {
                return default(bool);
            }
            bool? value = GetBool(data);
            if (value != null)
            {
                return value.Value;
            }
            bool result;
            return bool.TryParse(data.ToString(), out result) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(this object data)
        {
            if (data == null)
            {
                return null;
            }
            switch (data.ToString().Trim().ToLower())
            {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == null)
            {
                return default(bool?);
            }
                
            bool? value = GetBool(data);
            if (value != null)
            {
                return value.Value;
            }
            bool result;
            return bool.TryParse(data.ToString(), out result)? result:default(bool ?);
        }
        #endregion

        #region 字符串转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(this object data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            return data.ToString().Trim();
        }
        #endregion

        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            return value == null || IsEmpty(value.Value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
           return  value == Guid.Empty;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            return value == null || string.IsNullOrEmpty(value.ToString());
        }
    }
}
