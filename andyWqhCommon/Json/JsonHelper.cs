/*********************************************************************
 * Copyright © 2016 Fine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace andyWqhCommon.Json
{
   public  static class JsonHelper
    {
        /// <summary>
        /// 将json字符串发序列化成对象
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>
        public static object ToJson(this string strJson)
        {
            return string.IsNullOrEmpty(strJson) ? null : JsonConvert.DeserializeObject(strJson);
        }

        /// <summary>
        /// 将对象序列化成json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// 按指定时间格式将对象序列化成json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateTimeFormat">时间格式</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string dateTimeFormat)
        {
            var timeConverter = new IsoDateTimeConverter
            {
               DateTimeFormat =  dateTimeFormat
            };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// 将json字符串转换成泛型对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string strJson)
        {
            return string.IsNullOrEmpty(strJson) ? default(T) : JsonConvert.DeserializeObject<T>(strJson);
        }

        /// <summary>
        /// 将json字符串转换成泛型集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="strJosn">json字符串</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string strJosn)
        {
            return string.IsNullOrEmpty(strJosn)
                ? null
                : JsonConvert.DeserializeObject<List<T>>(strJosn);
        }

        /// <summary>
        /// 将json字符串转换成dataTable
        /// </summary>
        /// <param name="strJson">json字符串</param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string strJson)
        {
            return string.IsNullOrEmpty(strJson)
                ? null
                : JsonConvert.DeserializeObject<DataTable>(strJson);
        }

        public static JObject ToJObject(this string strJson)
        {
            return  string.IsNullOrEmpty(strJson)? JObject.Parse("{}") : JObject.Parse(strJson.Replace("&nbsp;", ""));
        }
    }
}

