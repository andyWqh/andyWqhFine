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
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon
{
    public class GZip
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="strText">字符串内容</param>
        /// <returns></returns>
        public static string Compress(string strText)
        {
            if(string.IsNullOrEmpty(strText))
            {
                return string.Empty;
            }
            byte[] buffter = Encoding.UTF8.GetBytes(strText);
            return Convert.ToBase64String(Compress(buffter));
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="text">文本</param>
        public static string Decompress(string strText)
        {
            if(string.IsNullOrEmpty(strText))
            {
                return string.Empty;
            }
            byte[] buffter = Convert.FromBase64String(strText);
            using (var ms = new MemoryStream(buffter))
            {
                using (var zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(zip))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] Compress(byte[] buffter)
        {
            if(buffter == null)
            {
                return null;
            }
            using (var ms = new MemoryStream())
            {
                using (var zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(buffter, 0, buffter.Length);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] Decompress(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            return Decompress(new MemoryStream(buffer));
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] Compress(Stream stream)
        {
            if (stream == null || stream.Length == 0)
            {
                return null;
            }
            return Compress(StreamToBytes(stream));
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] Decompress(Stream stream)
        {
            if (stream == null || stream.Length == 0)
                return null;
            using (var zip = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(zip))
                {
                    return Encoding.UTF8.GetBytes(reader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] StreamToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
