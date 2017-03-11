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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andyWqhCommon.Cache
{
    /// <summary>
    /// 缓存增删查操作
    /// </summary>
    public interface ICache
    {
        T GetCache<T>(string cacheKey) where T : class;

        void WriteCache<T>(T value, string cahceKey) where T : class;

        void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class;

        void RemoveCache(string cacheKey);

        void RemoveCache();
    }
}
