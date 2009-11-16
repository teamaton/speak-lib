using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Iesi.Collections.Generic;

namespace SpeakFriend.Utilities.Web
{
    /// <summary>
    /// Ermöglicht einen verallgemeinerten Zugriff auch Benutzerdaten, 
    /// sowohl für den web- als auch für eine allgemeinen Awendungskontext.
    /// </summary>
    public class SessionData
    {
    	private readonly HashedSet<string> _appDomainInsertedKeys = new HashedSet<string>();

        public object this[string key]
        {
            get
            {
				if (ContextUtil.IsWebContext)
				{
                    if(HttpContext.Current.Session == null)
                        throw new NullReferenceException("Probably you access session data to late or to early in the page life cycle.");

                    return HttpContext.Current.Session[key];
				}

                return AppDomain.CurrentDomain.GetData(key);
            }
            set
            {
				if (ContextUtil.IsWebContext)
				{
					HttpContext.Current.Session[key] = value;
				}
				else
				{
					AppDomain.CurrentDomain.SetData(key, value);
				}
				_appDomainInsertedKeys.Add(key);					
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

		public void Clear()
		{
			if (ContextUtil.IsWebContext)
				foreach (string key in _appDomainInsertedKeys)
					HttpContext.Current.Session.Remove(key);
			else
				foreach (string key in _appDomainInsertedKeys)
					AppDomain.CurrentDomain.SetData(key, null);

			_appDomainInsertedKeys.Clear();
		}

    	public void Remove(string key)
    	{
    		if (ContextUtil.IsWebContext)
    		{
    			HttpContext.Current.Session.Remove(key);
    		}
			else
    		{
				AppDomain.CurrentDomain.SetData(key, null);
    			_appDomainInsertedKeys.Remove(key);
    		}
    	}
    }
}
