using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Checks whether this Type inherits from or implements the given generic Type or Interface.
		/// </summary>
		/// <remarks>
		/// Praise goes to http://stackoverflow.com/questions/74616/how-to-detect-if-type-is-another-generic-type/75502#75502.
		/// </remarks>
		/// <param name="givenType"></param>
		/// <param name="genericType"></param>
		/// <returns></returns>
		public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
		{
			var interfaceTypes = givenType.GetInterfaces();

			if (interfaceTypes.Where(it => it.IsGenericType).Any(it => it.GetGenericTypeDefinition() == genericType))
			{
				return true;
			}

			Type baseType = givenType.BaseType;
			if (baseType == null) return false;

			return baseType.IsGenericType ?
				baseType.GetGenericTypeDefinition() == genericType :
				IsAssignableToGenericType(baseType, genericType);
		}
	}
}
