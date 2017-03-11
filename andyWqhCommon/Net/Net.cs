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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using andyWqhCommon.Extend;
using andyWqhCommon.Json;

namespace andyWqhCommon.Net
{
    /// <summary>
    /// 网络操作
    /// </summary>
    public class Net
    {
        public static string GetIP
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = GetWebClientIP();
                }
                if (result.IsEmpty())
                {
                    result = GetLanIP();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取web客户端IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientIP()
        {
            var ip = GetWebRemoteIP();
            foreach (var hostIP in Dns.GetHostAddresses(ip))
            {
                if (hostIP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostIP.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        /// <returns></returns>
        private static string GetWebRemoteIP()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                   HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        /// <returns></returns>
        private static string GetLanIP()
        {
            foreach (var hostIP in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostIP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostIP.ToString();
                }
            }
            return string.Empty;
        }

        public static string Host
        {
            get { return HttpContext.Current == null ? Dns.GetHostName() : GetWebClientHostName(); }
        }

        /// <summary>
        /// 获取web客户端主机名
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientHostName()
        {
            if (!HttpContext.Current.Request.IsLocal)
            {
                return string.Empty;
            }
            var ip = GetWebRemoteIP();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
            {
                result = Dns.GetHostName();
            }
            return result;
        }

        /// <summary>
        /// 返回描述本地计算机上的网络接口的对象(网络接口也称为网络适配器)。
        /// </summary>
        /// <returns></returns>
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        /// <summary>
        /// 通过NetWorkInterface读取网卡MAC
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacByNetWorkInterface()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }

        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetLocation(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return string.Empty;
            }
            string res = string.Empty;
            try
            {
                string url = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&dtype=json&key=b39857e36bee7a305d55cdb113a9d725";
                res = HttpMethods.HttpGet(url);
                var resjson = res.ToObject<ObjectX>();
                res = resjson.Result.Area + " " + resjson.Result.Location;
            }
            catch (Exception)
            {
                res = string.Empty;
                throw;
            }
            if (!string.IsNullOrEmpty(res))
            {
                return res;
            }
            try
            {
                string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
                res = HttpMethods.HttpGet(url, Encoding.GetEncoding("GBK"));
                var resjson = res.ToObject<BaiduObj>();
                res = resjson.DataList[0].Location;
            }
            catch
            {
                res = "";
            }
            return res;
        }

        /// <summary>
        /// 百度接口
        /// </summary>
        public class  BaiduObj
        {
           public  List<Dataone> DataList { get; set; }
        }
        public class  Dataone
        {
            public  string Location { get; set; }
        }

        /// <summary>
        /// 聚合数据
        /// </summary>
        public class ObjectX
        {
            public  string ResultCode { get; set; }

            public  DataoneEx Result { get; set; }

            public  string Reason { get; set; }

            public  string ErrorCode { get; set; }
        }

        public class DataoneEx
        {
            public string Area { get; set; }
            public string Location { get; set; }
        }

        /// <summary>
        /// 获取浏览器信息
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var browser = HttpContext.Current.Request.Browser;
                return string.Format("{0} {1}", browser.Browser, browser.Version);
            }
        }
    }
}
