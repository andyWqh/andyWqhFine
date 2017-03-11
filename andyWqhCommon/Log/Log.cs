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
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace andyWqhCommon.Log
{
    public class Log
    {
        private readonly ILog logger;

        public Log() { }

        public Log(ILog log)
        {
            this.logger = log;
        }

        public void Debug(object message)
        {
            this.logger.Debug(message);
        }

        public void Error(object message)
        {
            this.logger.Error(message);
        }

        public void Info(object message)
        {
            this.logger.Info(message);
        }

        public void Warn(object message)
        {
            this.logger.Warn(message);
        }
    }
}
