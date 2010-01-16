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

		/// <summary>
		/// Sets the field indicated by fieldName of the given obj to the given valueToSet. <br/>
		/// Uses the following BindingFlags: NonPublic | Instance | DeclaredOnly.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fieldName"></param>
		/// <param name="valueToSet"></param>
		/// <returns></returns>
		public static object SetPrivateField(this object obj, string fieldName, object valueToSet)
		{
			var flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

			obj.GetType().GetField(fieldName, flags).SetValue(obj, valueToSet);

			return obj;
		}

		public static object SetProperty(this object obj, string propertyName, object valueToSet)
		{
			var flags = BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic;

			obj.GetType().GetProperty(propertyName, flags).SetValue(obj, valueToSet, /*index*/ null);

			return obj;
		}
	}
}
