using System;

namespace SpeakFriend.Utilities
{
	public static class SettingExtensions
	{
		public static T EnumValue<T>(this SettingString setting)
		{
			if (typeof(T).BaseType != typeof(Enum))
				throw new ArgumentException("The type parameter must be of an Enum type!", "T");
			if (setting == null) throw new ArgumentNullException("setting");

			return (T) Enum.Parse(typeof (T), setting.Value);
		}

		public static SettingString SetEnumValue(this SettingString setting, Enum value)
		{
			if (setting == null) throw new ArgumentNullException("setting");
			if (value == null) throw new ArgumentNullException("value");

			setting.Value = value.ToString();
			return setting;
		}
	}
}