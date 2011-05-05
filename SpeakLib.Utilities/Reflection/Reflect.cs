﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Reflection
{
	/// <summary>
	/// Get members of a class in a type safe manner. Use the untyped version for static members.
	/// </summary>
	/// <remarks>
	/// Inspired by http://www.nickbutler.net/Article/StrongNames
	/// </remarks>
	public static class Reflect<TClass>
	{
		public static string MemberName<TMember>(Expression<Func<TClass, TMember>> m)
		{
			return Reflect.GetMemberInfo(m).Name;
		}

		/// <summary>
		/// Same as MemberName, just shorter.
		/// </summary>
		public static string Name<TMember>(Expression<Func<TClass, TMember>> m)
		{
			return MemberName(m);
		}

		public static FieldInfo Field<T>(Expression<Func<TClass, T>> m)
		{
			return Reflect.GetFieldInfo(m);
		}

		public static PropertyInfo Property<T>(Expression<Func<TClass, T>> m)
		{
			return Reflect.GetPropertyInfo(m);
		}

		public static MethodInfo Method(Expression<Action<TClass>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1>(Expression<Action<TClass, T1>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2>(Expression<Action<TClass, T1, T2>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3>(Expression<Action<TClass, T1, T2, T3>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4>(Expression<Action<TClass, T1, T2, T3, T4>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5>(Expression<Action<TClass, T1, T2, T3, T4, T5>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6>(Expression<Action<TClass, T1, T2, T3, T4, T5, T6>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6, T7>(Expression<Action<TClass, T1, T2, T3, T4, T5, T6, T7>> m)
		{
			return Reflect.GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Action<TClass, T1, T2, T3, T4, T5, T6, T7, T8>> m)
		{
			return Reflect.GetMethodInfo(m);
		}
	}

	/// <summary>
	/// Reflection for <b>static</b> members of any Type.
	/// </summary>
	public static class Reflect
	{
		internal static FieldInfo GetFieldInfo(LambdaExpression lambda)
		{
			return (FieldInfo)GetMemberInfo(lambda);
		}

		internal static PropertyInfo GetPropertyInfo(LambdaExpression lambda)
		{
			return (PropertyInfo)GetMemberInfo(lambda);
		}

		internal static MemberInfo GetMemberInfo(LambdaExpression lambda)
		{
			return ((MemberExpression)lambda.Body).Member;
		}

		internal static MethodInfo GetMethodInfo(LambdaExpression lambda)
		{
			return ((MethodCallExpression)lambda.Body).Method;
		}

		public static string Name<TMember>(Expression<Func<TMember>> m)
		{
			return MemberName(m);
		}

		public static string MemberName<TMember>(Expression<Func<TMember>> m)
		{
			return GetMemberInfo(m).Name;
		}

		public static FieldInfo Field<T>(Expression<Func<T>> m)
		{
			return GetFieldInfo(m);
		}

		public static PropertyInfo Property<T>(Expression<Func<T>> m)
		{
			return GetPropertyInfo(m);
		}

		public static MethodInfo Method(Expression<Action> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1>(Expression<Action<T1>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2>(Expression<Action<T1, T2>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3>(Expression<Action<T1, T2, T3>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4>(Expression<Action<T1, T2, T3, T4>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5>(Expression<Action<T1, T2, T3, T4, T5>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6>(Expression<Action<T1, T2, T3, T4, T5, T6>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6, T7>(Expression<Action<T1, T2, T3, T4, T5, T6, T7>> m)
		{
			return GetMethodInfo(m);
		}

		public static MethodInfo Method<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> m)
		{
			return GetMethodInfo(m);
		}
	}
}