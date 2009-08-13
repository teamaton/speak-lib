using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpeakFriend.Utilities.Reflection
{
	public static class ReflectionHelper
	{
		public static List<T> GetConstMembersAsList<T>(this Type type) where T : class
		{
			var fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
			var list = new List<T>();
			foreach (var fieldInfo in fieldInfos)
			{
				if (fieldInfo.FieldType != typeof(T)) continue;

				var value = fieldInfo.GetRawConstantValue();
				if (value is T)
					list.Add(value as T);
			}
			return list;
		}
	}
}
