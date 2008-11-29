using System;
using System.Collections;
using System.Collections.Generic;
using MergeSystem.Indexus.WinServiceCommon.Formatters;
using MergeSystem.Indexus.WinServiceCommon.Provider.Cache;

namespace SpeakFriend.Utilities.Web
{    
    public class Cache
    {
        private static readonly ICache _cache;
        
        public static int Count{ get { return _cache.Count; } }

        static Cache()
        {
            _cache = new CacheAspNet();
        }

        public static void Add(string key, object obj)
        {
            _cache.Add(key, obj);
        }

        public static void Add(string key, object obj, TimeSpan timeSpan)
        {
            _cache.Add(key, obj, timeSpan);
        }

        public static object Get(string key)
        {
            return _cache.Get(key);
        }

        public static Type Get<Type>(string key)
        {
            return _cache.Get<Type>(key);
        }

        public static void Clear()
        {
            _cache.Clear();
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        public static bool Contains(string key)
        {
            return _cache.Get(key) != null;
        }

        public static IDictionaryEnumerator GetEnumerator()
        {
            return _cache.GetEnumerator();
        }
    }
}
