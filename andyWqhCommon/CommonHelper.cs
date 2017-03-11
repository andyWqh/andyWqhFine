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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon
{
    /// <summary>
    /// 常用公共类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 计时器开始
        /// </summary>
        /// <returns></returns>
        public static Stopwatch TimerStart()
        {
            Stopwatch watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            return watch;
        }

        /// <summary>
        /// 计时器结束
        /// </summary>
        /// <param name="watch"></param>
        /// <returns></returns>
        public static string TimerEnd(Stopwatch watch)
        {
            watch.Stop();
            return watch.ElapsedMilliseconds + "";
        }

        /// <summary>
        /// 删除数组中重复项
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string[] RemoveDup(string[] array)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                if (!list.Contains(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 表示全局唯一标识符 (GUID)。
        /// </summary>
        /// <returns></returns>
        public static string GuId()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }

        /// <summary>
        /// 生成0-9随机数
        /// </summary>
        /// <param name="codeNum"></param>
        /// <returns></returns>
        public static string CreateRndNum(int codeNum)
        {
            StringBuilder sb = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < codeNum; i++)
            {
                int tem = rand.Next(0, 9);
                sb.AppendFormat("{0}", tem);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }
        /// <summary>
        /// 删除最后结尾的长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string DelLastLength(string str, int Length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Substring(0, str.Length - Length);
        }
    }
}
