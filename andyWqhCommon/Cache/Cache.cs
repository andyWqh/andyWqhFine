/*********************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: andyWqh
 * Description: Fine快速开发平台
 * Email:andyWqh@163.com
 * weixin:andysun199054
 * QQ:240463491
**********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace andyWqhCommon.Cache
{
    public class Cache:ICache
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        public T GetCache<T>(string cacheKey) where T : class
        {
            if (cache[cacheKey] != null)
            {
                return (T)cache[cacheKey];
            }
            return default(T);
        }

        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            cache.Insert(cacheKey,value,null,DateTime.Now.AddMinutes(10),System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            cache.Insert(cacheKey,value,null,expireTime,System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void RemoveCache(string cacheKey)
        {
            cache.Remove(cacheKey);
        }

        public void RemoveCache()
        {
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
