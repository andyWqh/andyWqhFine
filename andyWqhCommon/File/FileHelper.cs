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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NPOI.OpenXmlFormats.Dml;

namespace andyWqhCommon.File
{
    public class FileHelper
    {
        /// <summary>
        /// 检测指定文件目录是否存在
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static bool IsExistDirectory(string dirPath)
        {
            return Directory.Exists(dirPath);
        }

        /// <summary>
        /// 检测是定文件是否存在，如果存在则返回true
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExistFile(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public static string[] GetFileNames(string dirPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(dirPath))
            {
                throw new FileNotFoundException();
            }
            //获取文件列表
            return Directory.GetFiles(dirPath);
        }

        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        public static string[] GetDirectories(string dirPath)
        {
            try
            {
                return Directory.GetDirectories(dirPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定目录以及子目录中所有文件列表
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。</param>
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns></returns>
        public static string[] GetFileNames(string dirPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常信息提示
            if (!IsExistDirectory(dirPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(dirPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(dirPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检查指定目录是否为空
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string dirPath)
        {
            try
            {
                //判断是否存在文件
                string[] fileNames = GetFileNames(dirPath);
                if (fileNames != null && fileNames.Length > 0)
                {
                    return false;
                }
                //判断是否存在文件夹
                string[] directoryNames = GetDirectories(dirPath);
                if (directoryNames != null && directoryNames.Length > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                //这里记录日志
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return true;
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>  
        public static bool Contains(string dirPath, string searchPattern)
        {
            try
            {
                //获取指定文件列表
                string[] fileNames = GetFileNames(dirPath, searchPattern, false);
                //判断指定文件是否存在
                return fileNames != null && fileNames.Length > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool Contains(string dirPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定文件列表
                string[] fileNames = GetFileNames(dirPath, searchPattern, isSearchChild);
                //判断指定文件是否存在
                return fileNames != null && fileNames.Length > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirPath">要创建的目录路径包括目录名</param>
        public static void CreateDir(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return;
            }
            if (!Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPath))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPath);
            }
        }

        /// <summary>
        /// 删除指定路径目录
        /// </summary>
        /// <param name="dirPath">指定要删除的目录路径和名称</param>
        public static void DeleteDir(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return;
            }
            if (Directory.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPath))
            {
                Directory.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPath);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">指定要删除文件路径和名称</param>
        public static void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + fileName))
            {
                System.IO.File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + fileName);
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="dirPath">带后缀的文件名</param>
        /// <param name="fileContent">文件内容</param>
        public static void CreateFile(string dirPath, string fileContent)
        {
            if (string.IsNullOrEmpty(dirPath))
            {
                return;
            }
            dirPath = dirPath.Replace("/", "\\");
            if (dirPath.IndexOf("\\") > -1)
            {
                CreateDir(dirPath.Substring(0, dirPath.LastIndexOf("\\")));
            }
            StreamWriter sw = new StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPath, false, Encoding.GetEncoding("GB2312"));
            sw.Write(fileContent);
            sw.Close();
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void CreateFileContent(string path, string content)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            FileInfo fielInfo = new FileInfo(path);
            var dir = fielInfo.Directory;
            if (!dir.Exists)
            {
                dir.Create();
            }
            StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
            sw.Write(content);
            sw.Close();
        }

        /// <summary>
        /// 移动文件(剪贴--粘贴)
        /// </summary>
        /// <param name="dirPathOrg">要移动的文件的路径及全名(包括后缀)</param>
        /// <param name="dirPathNew">文件移动到新的位置,并指定新的文件名</param>
        public static void MoveFile(string dirPathOrg, string dirPathNew)
        {
            dirPathOrg = dirPathOrg.Replace("/", "\\");
            dirPathNew = dirPathNew.Replace("/", "\\");
            if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathOrg))
            {
                System.IO.File.Move(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathOrg, HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathNew);
            }
        }

        /// <summary>
        /// 赋值文件
        /// </summary>
        /// <param name="dirPathOrg">要移动的文件的路径及全名(包括后缀)</param>
        /// <param name="dirPathNew">文件移动到新的位置,并指定新的文件名</param>
        public static void CopyFile(string dirPathOrg, string dirPathNew)
        {
            dirPathOrg = dirPathOrg.Replace("/", "\\");
            dirPathNew = dirPathNew.Replace("/", "\\");
            if (System.IO.File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathOrg))
            {
                System.IO.File.Copy(HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathOrg, HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dirPathNew);
            }
        }
        /// <summary>
        /// 根据时间得到目录名yyyyMMdd
        /// </summary>
        /// <returns></returns>
        public static string GetDateDir()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 根据时间得到文件名HHmmssff
        /// </summary>
        /// <returns></returns>
        public static string GetDateFile()
        {
            return DateTime.Now.ToString("HHmmssff");
        }

        /// <summary>
        /// 根据时间获取指定路径的 后缀名的 的所有文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="Extension">后缀名 比如.txt</param>
        /// <retu
        public static DataRow[] GetFileByTime(string path, string Extension)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (Directory.Exists(path))
            {
                string fileExts = string.Format("*{0}", Extension);
                string[] files = Directory.GetFiles(path, fileExts);
                if (files != null && files.Length > 0)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add(new DataColumn("fileName", Type.GetType("System.String")));
                    table.Columns.Add(new DataColumn("createTime", Type.GetType("System.DateTime")));
                    for (int i = 0; i < files.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        DateTime createTime = System.IO.File.GetCreationTime(files[i]);
                        string fileName = Path.GetFileName(files[i]);
                        row["fileName"] = fileName;
                        row["createTime"] = createTime;
                        table.Rows.Add(row);
                    }
                    return table.Select(string.Empty, "createTime desc");
                }
            }
            return new DataRow[0];
        }

        /// <summary>
        /// 复制文件夹(递归)
        /// </summary>
        /// <param name="fromDir">源文件夹路径</param>
        /// <param name="toDir">目标文件夹路径</param>
        public static void CopyFolder(string fromDir, string toDir)
        {
            Directory.CreateDirectory(toDir);
            if (!Directory.Exists(fromDir))
            {
                return;
            }
            string[] directories = Directory.GetDirectories(fromDir);
            if (directories != null && directories.Length > 0)
            {
                foreach (var dir in directories)
                {
                    CopyFolder(dir, toDir + dir.Substring(dir.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(fromDir);
            if (files != null && files.Length > 0)
            {
                foreach (var file in files)
                {
                    System.IO.File.Copy(file, toDir + file.Substring(file.LastIndexOf("\\")), true);
                }
            }
        }
        /// <summary>
        /// 检查文件,如果文件不存在则创建  
        /// </summary>
        /// <param name="FilePath">路径,包括文件名</param>
        public static void ExistsFile(string FilePath)
        {
            //if(!File.Exists(FilePath))    
            //File.Create(FilePath);    
            //以上写法会报错,详细解释请看下文.........   
            if (!System.IO.File.Exists(FilePath))
            {
                FileStream fs = System.IO.File.Create(FilePath);
                fs.Close();
            }
        }

        /// <summary>
        /// 删除指定文件夹对应其他文件夹里的文件
        /// </summary>
        /// <param name="fromDir">指定文件夹路径</param>
        /// <param name="toDir">对应其他文件夹路径</param>
        public static void DeleteFolderFiles(string fromDir, string toDir)
        {
            Directory.CreateDirectory(toDir);

            if (!Directory.Exists(fromDir)) return;

            string[] directories = Directory.GetDirectories(fromDir);

            if (directories.Length > 0)
            {
                foreach (string dir in directories)
                {
                    DeleteFolderFiles(dir, toDir + dir.Substring(dir.LastIndexOf("\\")));
                }
            }


            string[] files = Directory.GetFiles(fromDir);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    System.IO.File.Delete(toDir + file.Substring(file.LastIndexOf("\\")));
                }
            }
        }
        /// <summary>
        /// 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileName(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }

        /// <summary>
        /// 复制文件参考方法,页面中引用
        /// </summary>
        /// <param name="cDir">新路径</param>
        /// <param name="TempId">模板引擎替换编号</param>
        public static void CopyFiles(string cDir, string TempId)
        {
            //if (Directory.Exists(Request.PhysicalApplicationPath + "\\Controls"))
            //{
            //    string TempStr = string.Empty;
            //    StreamWriter sw;
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Default.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\List.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\View.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\DissList.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Diss.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Review.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx"))
            //    {
            //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx");
            //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

            //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Search.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
            //        sw.Write(TempStr);
            //        sw.Close();
            //    }
            //}
        }

        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// 创建一个文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ;
            }
        }

        /// <summary>
        /// 获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static long GetFileSize(string filePath)
        {
            //创建一个文件对象
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小
            return fi.Length;
        }

        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="size">初始文件大小</param>
        /// <returns></returns>
        public static string ToFileSize(long size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";
            return m_strSize;
        }

        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            System.IO.File.WriteAllText(filePath, text, encoding);
        }
        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            System.IO.File.AppendAllText(filePath, content);
        }

        /// <summary>
        /// 将源文件的内容复制到目标文件中
        /// </summary>
        /// <param name="sourceFilePath">源文件的绝对路径</param>
        /// <param name="destFilePath">目标文件的绝对路径</param>
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            System.IO.File.Copy(sourceFilePath, destFilePath, true);
        }

        /// <summary>
        /// 将文件移动到指定目录
        /// </summary>
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                   DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }
                //将文件移动到指定目录
                System.IO.File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }

        /// <summary>
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }

        /// <summary>
        /// 从文件的绝对路径中获取扩展名
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }

        /// <summary>
        /// 清空指定目录下所有文件及子目录,但该目录依然保存.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            directoryPath = HttpContext.Current.Server.MapPath(directoryPath);
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }
                //删除目录中所有的子目录
                string[] directoryNames = GetDirectories(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDir(directoryNames[i]);
                }
            }
        }
        /// <summary>
        /// 清空文件内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            //删除文件
            System.IO.File.Delete(filePath);

            //重新创建该文件
            CreateFile(filePath);
        }

        /// <summary>
        /// 删除指定目录及其所有子目录
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void DeleteDirectory(string directoryPath)
        {
            directoryPath = HttpContext.Current.Server.MapPath(directoryPath);
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        /// <summary>
        /// 本地路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }
}
