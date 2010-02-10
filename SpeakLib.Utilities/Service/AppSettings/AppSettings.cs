using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SpeakFriend.Utilities
{
    public abstract class AppSettings
    {
		protected static readonly AppSettingsReader SettingReader = new AppSettingsReader();

		/// <summary>
		/// Returns a given value from the AppSettings section of the web/app.config it it exists;
		/// throws an <see cref="InvalidOperationException"/> if the key does not exist.
		/// </summary>
        public static T Get<T>(string key)
        {
            return (T)SettingReader.GetValue(key, typeof(T));
        }

		/// <summary>
		/// Returns the value for the given key if it exists, or the defaultValue. <br/>
		/// Slow for many calls bc of exception usage.
		/// </summary>
		public static T TryGet<T>(string key, T defaultValue)
		{
			try
			{
				return Get<T>(key);
			}
			catch (InvalidOperationException)
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Returns a given value from the AppSettings section of the web/app.config it it exists;
		/// else returns the default value of the given type T (not an exception!). <br/>
		/// Use this method if you want to cover the case of non-existing config values. <br />
		/// Conversion to the most common types implemented.
		/// </summary>
		public static T Get_2<T>(string key)
		{
			var val = ConfigurationManager.AppSettings[key];
			var type = typeof (T);
			if (type == typeof(string))
				return (T)(object)val;
			if (type == typeof(bool))
				return (T)(object)Convert.ToBoolean(val);
			if (type == typeof(int))
				return (T)(object)Convert.ToInt32(val);

			throw new ArgumentException(
				string.Format("Conversion to the given Type {0} has not yet been implemented!", typeof(T)), "T" );
		}

		/// <summary>
		/// Returns a given value from the AppSettings section of the web/app.config it it exists;
		/// else returns the given defaultValue. <br/>
		/// Conversion to the most common types implemented.
		/// </summary>
		public static T TryGet_2<T>(string key, T defaultValue)
		{
			var val = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrEmpty(val))
				return defaultValue;
			var type = typeof (T);
			if (type == typeof(string))
				return (T)(object)val;
			if (type == typeof(bool))
				return (T)(object)Convert.ToBoolean(val);
			if (type == typeof(int))
				return (T)(object)Convert.ToInt32(val);

			throw new ArgumentException(
				string.Format("Conversion to the given Type {0} has not yet been implemented!", typeof(T)), "T" );
		}

    	private static string _applicationPath = null;
		/// <summary>
		/// Returns the root path of the currently running application incl. web.
		/// </summary>
		public static string ApplicationPath
		{
			get
			{
				if (_applicationPath == null)
				{
					if (HttpContext.Current != null)
					{
						// don't use HttpContext.Current.Request.PhysicalApplicationPath
						// bc it doesn't work without a Request (e.g. in Application_Start)
						_applicationPath = HttpContext.Current.Server.MapPath("/");
					}
					else
					{
						_applicationPath = Get<string>("ApplicationPath");
					}
				}

				return _applicationPath;
			}
		}

    	protected static void EnsurePaths(Type type)
    	{
    		foreach (var prop in type.GetProperties().Where(prop => prop.Name.EndsWith("Absolute")))
    		{
    			var path = prop.GetValue(null, null) as string;

    			path.EnsureEndsWith("/").EnsurePathExists();
    		}
    	}

    	public static string PathCombineRobust(string partOne, string partTwo)
    	{
    		return Path.Combine(partOne.EnsureEndsNotWith(Path.DirectorySeparatorChar.ToString()),
    		                    partTwo.EnsureStartsNotWith("/"));
    	}

		protected static string GetAbsolute(string relativePath)
		{
			return PathCombineRobust(ApplicationPath, relativePath);
		}
    }
}
