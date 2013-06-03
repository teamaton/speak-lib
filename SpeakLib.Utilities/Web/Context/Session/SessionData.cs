using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Iesi.Collections.Generic;

namespace SpeakFriend.Utilities.Web
{
	/// <summary>
	/// Ermöglicht einen verallgemeinerten Zugriff auf Benutzerdaten, 
	/// sowohl für den Web- als auch für einen allgemeinen Anwendungskontext.
	/// </summary>
	[Serializable]
	public class SessionData
	{
		private const string _versionKey = "VERSION";
		private const int _version = 1;
		private readonly Iesi.Collections.Generic.ISet<string> _appDomainInsertedKeys = new HashedSet<string>();

		public object this[string key]
		{
			get
			{
				if (ContextUtil.IsWebContext)
				{
					if (HttpContext.Current.Session == null)
						throw new NullReferenceException(
							"Probably you're accessing session data too late or too early in the page life cycle!");

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

		/// <summary>
		/// Use only if truly necessary; else simply use <see cref="Get{T}(string,System.Func{T})"/>.
		/// </summary>
		public bool Exists(string key)
		{
			return this[key] != null;
		}

		/// <summary>
		/// Returns the untyped item for the given key. May be <b>null</b>.
		/// </summary>
		public object Get(string key)
		{
			return this[key];
		}

		/// <summary>
		/// Returns the typed item for the given key. May be <b>null</b>.
		/// Cannot use this for value types because an exception would be thrown if the value does not exist.
		/// <br/>
		/// Consider using <see cref="Get{T}(string,T)"/> for value types, or e.g. Get(key, (int?) null) for nullables.
		/// </summary>
		public T Get<T>(string key) where T : class
		{
			return (T) this[key];
		}

		/// <summary>
		/// Returns the item for the given key. 
		/// <br/>
		/// If the key does not exist, session will be initialized 
		/// with the given initialValue &amp; the initialValue will 
		/// be returned.
		/// <br/>
		/// Consider using <see cref="Get{T}(string,System.Func{T})"/> for better performance.
		/// </summary>
		public T Get<T>(string key, T initialValue)
		{
			return Get(key, () => initialValue);
		}

		/// <summary>
		/// Returns the item for the given key. 
		/// <br/>
		/// If the key does not exist, session will be initialized with 
		/// the return value of the given initializer &amp; that value will 
		/// be returned.
		/// <br/>
		/// Consider using <see cref="Get{T}(string,T)"/> for value types.
		/// </summary>
		public T Get<T>(string key, Func<T> initializer)
		{
			var val = Get(key);

			if (val != null)
				return (T) val;

			var initialValue = initializer();
			this[key] = initialValue;
			return initialValue;
		}

		public void Clear()
		{
			if (ContextUtil.IsWebContext)
				foreach (var key in _appDomainInsertedKeys)
					HttpContext.Current.Session.Remove(key);
			else
				foreach (var key in _appDomainInsertedKeys)
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

		public void Clear(string[] dataPrefixes)
		{
			if (ContextUtil.IsWebContext)
			{
				var matchingKeys =
					HttpContext.Current.Session.Keys.Cast<string>().Where(key => dataPrefixes.Any(key.StartsWith)).ToList();
				foreach (var key in matchingKeys)
					HttpContext.Current.Session.Remove(key);
			}
			else
			{
				var appDomain = AppDomain.CurrentDomain;

				var fieldInfo = appDomain.GetType().GetField("_LocalStore", BindingFlags.NonPublic | BindingFlags.Instance);
				if (fieldInfo == null)
					return;

				var localStore = fieldInfo.GetValue(appDomain) as Dictionary<string, object[]>;
				if (localStore == null)
					return;

				var matchingKeys = localStore.Keys.Where(key => dataPrefixes.Any(key.StartsWith)).ToList();
				foreach (var key in matchingKeys)
					appDomain.SetData(key, null);
			}
		}

		public static void SetVersion()
		{
			HttpContext.Current.Session[_versionKey] = _version;
		}

		public static bool IsOldVersion()
		{
			if (HttpContext.Current == null || HttpContext.Current.Session == null)
				return true;

			var version = HttpContext.Current.Session[_versionKey];
			if (version != null)
			{
				return (int) version < _version;
			}

			return true;
		}
	}
}