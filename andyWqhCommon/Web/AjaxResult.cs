/*********************************************************************
 * Copyright © 2017 Fine.Framework 版权所有
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

namespace andyWqhCommon.Web
{
    public class AjaxResult
    {
        /// <summary>
        /// 操作结果类型，true or false , 1 or 0 and so on
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// 获取提醒消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取返回数据信息
        /// </summary>
        public object ReturnData { get; set; }

        public enum ResultType
        {
            /// <summary>
            /// 消息结果类型
            /// </summary>
            info,
            /// <summary>
            /// 成功结果类型
            /// </summary>
            success,
            /// <summary>
            /// 警告提醒类型
            /// </summary>
            warning,
            /// <summary>
            /// 异常结果类型
            /// </summary>
            error
        }
    }
}
