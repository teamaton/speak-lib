using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace SpeakFriend.Utilities.Web
{
    /// <summary>
    /// Ermöglicht einen verallgemeinerten Zugriff auch Benutzerdaten, 
    /// sowohl für den web- als auch für eine allgemeinen Awendungskontext.
    /// </summary>
    public class SessionData
    {
        public object this[string index]
        {
            get
            {
                if (ContextUtil.IsWebContext)
                    return HttpContext.Current.Session[index];

                return AppDomain.CurrentDomain.GetData(index);
            }
            set
            {
                if (ContextUtil.IsWebContext)
                    HttpContext.Current.Session[index] = value;

                AppDomain.CurrentDomain.SetData(index, value);
            }
        }

        public bool Exists(string key)
        {
            return this[key] != null;
        }

        /// <summary>
        /// Returns the item for the given key. May be <b>null</b>.
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
        /// <br/>
        /// If the key does not exist, session will be initialized 
        /// with the given initialValue &amp; the initialValue will 
        /// be returned.
        /// <br/>
        /// Consider using <see cref="GetInitialized{T}(string)"/> for better performance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        public T Get<T>(string key, object initialValue)
        {
            return Get(key, () => (T) initialValue);
        }

        public T Get<T>(string key, Func<T> initializer)
        {
            if (!Exists(key))
            {
                var initialValue = initializer();
                this[key] = initialValue;
                return initialValue;
            }

            return Get<T>(key);
        }

        public T Get<T>(string key, bool initialValue)
        {
            return Get<T>(key, (object)initialValue);
        }

        /// <summary>
        /// Returns the item for the given key, if it exists, else returns a new instance of 
        /// <typeparamref name="T">Type</typeparamref>.       
        /// </summary>
        /// <typeparam name="T">Needs to implement parameterless constructor.</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetInitialized<T>(string key) where T:new()
        {
            if(!Exists(key))
                this[key] = new T();    

            return Get<T>(key);
        }
    }
}
