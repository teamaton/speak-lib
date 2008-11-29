using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MergeSystem.Indexus.WinServiceCommon.Formatters;
using MergeSystem.Indexus.WinServiceCommon.Provider.Cache;

namespace SpeakFriend.Utilities.Web
{
    internal class CacheIndexus : ICache
    {
        private static readonly IndexusProviderBase _cache = IndexusDistributionCache.SharedCache;

        public int Count{ get { return Convert.ToInt32(_cache.Count); } }

        public IDictionaryEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public void Add(string key, object obj)
        {
            _cache.Add(key, obj);
        }

        public void Add(string key, object obj, TimeSpan expiration)
        {
            _cache.Add(key, obj, DateTime.UtcNow.Add(expiration));
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public Type Get<Type>(string key)
        {
            return _cache.Get<Type>(key);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}
