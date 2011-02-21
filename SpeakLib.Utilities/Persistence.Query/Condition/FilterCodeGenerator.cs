using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// This class is a helper, to create 
	/// copy and paste code for the Filter
	/// </summary>
	public class FilterCodeGenerator
	{
		private static readonly List<string> _membersToGenerate = new List<string>();
		private static readonly List<string> _constructorStatementsToGenerate = new List<string>();

		public static void Start(Type type)
		{
			var members = type.GetMembers();

			var listInitString = new StringBuilder();

			foreach (var member in members)
			{
				foreach (var attribute in member.GetCustomAttributes(true))
				{
					if (attribute.GetType() == typeof (FilterBooleanAttribute))
						AddToGenerate(member, "ConditionBoolean");

					if (attribute.GetType() == typeof (FilterIntegerAttribute))
						AddToGenerate(member, "ConditionInteger");

					if (attribute.GetType() == typeof (FilterSingleAttribute))
						AddToGenerate(member, "ConditionSingle");

					if (attribute.GetType() == typeof (FilterDecimalAttribute))
						AddToGenerate(member, "ConditionDecimal");

					if (attribute.GetType() == typeof (FilterBooleanAttribute) ||
					    attribute.GetType() == typeof (FilterIntegerAttribute) ||
					    attribute.GetType() == typeof (FilterSingleAttribute) ||
					    attribute.GetType() == typeof (FilterDecimalAttribute))

						listInitString.Append(member.Name).Append(",\r\n");
				}
			}
			foreach (var memberToGenerate in _membersToGenerate)
				Console.WriteLine(memberToGenerate);

			Console.WriteLine();
			Console.WriteLine();

			foreach (var statementToGenerate in _constructorStatementsToGenerate)
				Console.WriteLine(statementToGenerate);

			Console.WriteLine("AllConditions = new List<Condition> {0};", "{" + listInitString + "}");
		}

		private static void AddToGenerate(MemberInfo member, string className)
		{
			_membersToGenerate.Add("public " + className + " " + member.Name + ";");
			_constructorStatementsToGenerate.Add(
				String.Format("{0} = new " + className + "(this, \"{1}\");", member.Name, member.Name));
		}
	}
}