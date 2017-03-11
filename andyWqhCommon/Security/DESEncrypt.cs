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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace andyWqhCommon.Security
{
    /// <summary>
    /// DES加密，解密帮助类
    /// </summary>
   public class DESEncrypt
    {
        private static readonly string DESKey = "fine_desencrypt_andyWqh";

        #region =====加密=======

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, DESKey);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputBytesArray = Encoding.Default.GetBytes(text);
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey,"md5").Substring(0,8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms,des.CreateDecryptor(),CryptoStreamMode.Write);
            cs.Write(inputBytesArray,0,inputBytesArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        #endregion

        #region ======解密====

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return Descrypt(text, DESKey);
            }
            else
            {
                return default(string);
            }
        }

        public static string Descrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            if (string.IsNullOrEmpty(text))
            {
                return default(string);
            }
            int len = text.Length/2;
            byte[] inputByteArray = new byte[len];
            int i;
            for (int x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x*2,2),16);
                inputByteArray[x] = (byte) i;
            }
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
    }
}
