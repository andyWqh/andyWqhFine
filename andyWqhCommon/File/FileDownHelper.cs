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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace andyWqhCommon.File
{
    public class FileDownHelper
    {
        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="fileName">文件逻辑路径名称</param>
        /// <returns></returns>
        public static string FileNameExtension(string fileName)
        {
            return string.IsNullOrEmpty(fileName)
                ? string.Empty
                : Path.GetExtension(MapPathFile(fileName));
        }

        /// <summary>
        /// 获取文件的物理路径
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        private static string MapPathFile(string fileName)
        {
            return HttpContext.Current.Server.MapPath(fileName);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static bool FileExists(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && System.IO.File.Exists(fileName);
        }

        /// <summary>
        /// 根据文件名下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="name">名称</param>
        public static void DownLoadOld(string fileName, string name)
        {
            if (string.IsNullOrEmpty(fileName) || !FileExists(fileName))
            {
                return;
            }
            else
            {
                FileInfo fileInfo = new FileInfo(fileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.AppendHeader("Content-Disposition",
                    "attachment;fileName=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.AppendHeader("Content-Length", fileInfo.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(fileName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileName"></param>
        public static void DownLoad(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            string filePath = MapPathFile(fileName);
            //指定内存块大小
            long chunkSize = 204800;
            //建立200k的缓存区
            byte[] buffer = new byte[chunkSize];
            long dataToRead = 0;
            FileStream fs = null;
            try
            {
                //打开文件
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dataToRead = fs.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition",
                    "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());
                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = fs.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                HttpContext.Current.Response.Close();
            }
        }

        public static bool ResponseFile(HttpRequest request, HttpResponse response, string fileName, string fullPath,
            long speed)
        {
            try
            {
                FileStream myFileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader binaryReader = new BinaryReader(myFileStream);
                try
                {
                    response.AppendHeader("Accept-Ranges", "bytes");
                    response.Buffer = false;
                    long fileLength = myFileStream.Length;
                    long startBytes = 0;
                    int pack = 10240; //10k bytes
                    int sleep =
                        (int) Math.Floor((double) (1000*pack/speed)) + 1;
                    if (request.Headers["Range"] != null)
                    {
                        response.StatusCode = 206;
                        string[] range = request.Headers["Range"].Split(new char[] {'=', '-'});
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes > 0)
                    {
                        response.AppendHeader("Content-Range",
                            string.Format("bytes{0}-{1}", startBytes, fileLength - 1, fileLength));
                    }
                    response.AddHeader("Connection", "Keep-Alive");
                    response.ContentType = "application/octer-stream";
                    response.AddHeader("Content-Disposition",
                        "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));

                    binaryReader.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int) Math.Floor((double) ((fileLength - startBytes)/pack)) + 1;
                    for (int i = 0; i < maxCount; i++)
                    {
                        if (response.IsClientConnected)
                        {
                            response.BinaryWrite(binaryReader.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    binaryReader.Close();
                    myFileStream.Close();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
