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

		#region Property

		/// <summary>
		/// Set the property of the given object to the given value.
		/// </summary>
		/// <returns>The given object instance.</returns>
		public static object Property(this object obj, string propertyName, object valueToSet)
		{
			obj.PropertyInfo(propertyName).SetValue(obj, valueToSet, /*index*/ null);

			return obj;
		}

		/// <summary>
		/// Read the value of the property with the given name.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="propertyName"></param>
		/// <returns>The property value.</returns>
		public static object Property(this object obj, string propertyName)
		{
			var value = obj.PropertyInfo(propertyName).GetValue(obj, /*index*/ null);

			return value;
		}

		/// <summary>
		/// Read the value of the property with the given name and cast it to the given TResult type.
		/// </summary>
		/// <returns>The property value.</returns>
		public static TResult Property<TResult>(this object obj, string propertyName)
		{
			return (TResult) obj.Property(propertyName);
		}

		/// <summary>
		/// Returns the PropertyInfo object for the property with the given name.
		/// </summary>
		public static PropertyInfo PropertyInfo(this object obj, string propertyName)
		{
			const BindingFlags flags = BindingFlags.Static | BindingFlags.Instance |
			                           BindingFlags.Public | BindingFlags.NonPublic;

			return obj.GetType().GetProperty(propertyName, flags);
		}

		#endregion

		#region Field

		/// <summary>
		/// Set the field of the given object to the given value.
		/// </summary>
		/// <returns>The given object instance.</returns>
		public static object Field(this object obj, string fieldName, object valueToSet)
		{
			obj.FieldInfo(fieldName).SetValue(obj, valueToSet);

			return obj;
		}

		/// <summary>
		/// Read the value of the field with the given name.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fieldName"></param>
		/// <returns>The field value.</returns>
		public static object Field(this object obj, string fieldName)
		{
			var value = obj.FieldInfo(fieldName).GetValue(obj);

			return value;
		}

		/// <summary>
		/// Read the value of the field with the given name and cast it to the given TResult type.
		/// </summary>
		/// <returns>The field value.</returns>
		public static TResult Field<TResult>(this object obj, string fieldName)
		{
			return (TResult) obj.Field(fieldName);
		}

		/// <summary>
		/// Returns the FieldInfo object for the field with the given name.
		/// </summary>
		public static FieldInfo FieldInfo(this object obj, string fieldName)
		{
			const BindingFlags flags = BindingFlags.Static | BindingFlags.Instance |
			                           BindingFlags.Public | BindingFlags.NonPublic;

			return obj.GetType().GetField(fieldName, flags);
		}

		public static IEnumerable<FieldInfo> FieldInfos(this Type type, List<Type> matchTypes, Func<string, bool> matchFunc)
		{
			return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
				.WhereSafe(fi => matchFunc(fi.Name) && matchTypes.Contains(fi.FieldType));
		}

		#endregion

		public static object CallMethod(object cacheItem, string methodName)
		{
			var method = cacheItem.GetType().GetMethod(methodName);
			if (method == null)
				return null;

			return cacheItem.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, cacheItem, new object[0]);
		}
	}
}
