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

		/// <summary>
		/// Returns a mapping of all Member names of a class with the given Attribute instance of Type T. 
		/// </summary>
		/// <typeparam name="T">The Type of the Attribute class</typeparam>
		/// <param name="type">The Type of which to get the members</param>
		public static Dictionary<string, T> GetMembersWithAttribute<T>(this Type type)
		{
			MemberInfo[] properties = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

			var dictionary = new Dictionary<string, T>();

			foreach (var propertyInfo in properties)
			{
				foreach (object attribute in propertyInfo.GetCustomAttributes(false))
				{
					if (attribute is T)
						dictionary.Add(propertyInfo.Name, (T) attribute);
				}
			}

			return dictionary;
		}
	}
}
