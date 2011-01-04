namespace SpeakFriend.Utilities
{
	public class AppSetting<T>
	{
		public string Name { get; private set; }
		public T Value
		{
			get { return AppSettings.Get_2<T>(Name); }
			private set { AppSettings.Set(Name, value); }
		}

		public AppSetting(string name)
		{
			Name = name;
		}

		/// <summary>
		/// This makes it possible to keep using Settings.MyAppSetting instead of Settings.MyAppSetting.Value.
		/// </summary>
		public static implicit operator T(AppSetting<T> setting)
		{
			return setting.Value;
		}

		public void Set(T value)
		{
			Value = value;
		}
	}
}