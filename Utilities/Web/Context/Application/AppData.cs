﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class AppData
    {
        public bool IsWebContext
        {
            get { return HttpContext.Current != null; }
        }

        public object this[string index]
        {
            get
            {
                if (IsWebContext)
                    return HttpContext.Current.Application[index];

                return AppDomain.CurrentDomain.GetData(index);
            }
            set
            {
                if (IsWebContext)
                    HttpContext.Current.Application[index] = value;

                AppDomain.CurrentDomain.SetData(index, value);
            }
        }

        public bool Exists(string key)
        {
            return this[key] != null;
        }

        /// <summary>
        /// Returns the item for the given key. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return (T)this[key];
        }

        /// <summary>
        /// Returns the item for the given key. 
        /// 
        /// If the key does not exist, session will be initialized 
        /// with the given initialValue & the the initialValue will 
        /// be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        public T Get<T>(string key, object initialValue)
        {
            if (!Exists(key))
            {
                this[key] = initialValue;
                return (T)initialValue;
            }

            return Get<T>(key);
        }
    }
}
