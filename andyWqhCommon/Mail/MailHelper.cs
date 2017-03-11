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
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace andyWqhCommon.Mail
{
    public class MailHelper
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string MailUserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string MailPassword { get; set; }

        /// <summary>
        /// 邮件名称
        /// </summary>
        public string MailName { get; set; }

        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="toAddress">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="bodyContentm">征文内容</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBodyHtml">是否Html格式</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns></returns>
        public bool SendMail(string toAddress, string subject, string bodyContentm, string encoding = "UTF-8",
            bool isBodyHtml = true, bool enableSsl = false)
        {
            try
            {
                MailMessage message = new MailMessage();
                //接收人邮箱地址
                message.To.Add(new MailAddress(toAddress));
                //发送人账户
                message.From = new MailAddress(MailUserName, MailName);
                //主题编码方式
                message.SubjectEncoding = Encoding.GetEncoding(encoding);
                message.Subject = subject;
                //正文编码
                message.BodyEncoding = Encoding.GetEncoding(encoding);
                message.Body = bodyContentm;
                message.IsBodyHtml = isBodyHtml;

                //客户端绑定验证服务器地址和端口
                SmtpClient smtpClient = new SmtpClient(MailServer, 25);
                smtpClient.Credentials = new NetworkCredential(MailName, MailPassword);
                //SSL连接
                smtpClient.EnableSsl = enableSsl;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendByThread(string toAddress, string title, string body, int port = 25,bool isBodyHtml = true)
        {
            new Thread(new ThreadStart(delegate()
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient();
                    //邮箱smtpClient地址
                    smtpClient.Host = MailServer;
                    //端口号
                    smtpClient.Port = port;
                    //构建发件人身份凭据验证
                    smtpClient.Credentials = new NetworkCredential(MailUserName,MailPassword);
                    //构建消息类
                    MailMessage message = new MailMessage();
                    //设置优先级
                    message.Priority = MailPriority.High;
                    //消息发送人账号
                    message.From = new MailAddress(MailUserName,MailName,Encoding.UTF8);
                    //收件人
                    message.To.Add(toAddress);
                    //标题
                    message.Subject = string.IsNullOrEmpty(title) ? string.Empty : title.Trim();
                    //标题字符编码
                    message.SubjectEncoding = Encoding.UTF8;
                    //正文
                    message.Body = string.IsNullOrEmpty(body) ? string.Empty : body.Trim();
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = isBodyHtml;

                    //发送
                    smtpClient.Send(message);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            })).Start();

        }
    }
}
