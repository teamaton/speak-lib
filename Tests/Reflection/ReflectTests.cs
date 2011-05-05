using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using SpeakFriend.Utilities.Reflection;

namespace Tests.Reflection
{
	class Class
	{
		public static int StaticField = 42;
		public static int StaticProperty { get; set; }
		public static void StaticAction() { }
		public static void StaticAction(int i) { }
		public static int StaticFunc() { return 42; }
		public static int StaticFunc(int i) { return i; }

		public int Field = 42;
		public int Property { get; set; }
		public void Action() { }
		public void Action(int i) { }
		public int Func() { return 42; }
		public int Func(int i) { return i; }
	}

	public static class TestStaticClass
	{
		public static string String1;
		public static string String2;

		static TestStaticClass()
		{
			Console.WriteLine("TestStaticClass static c'tor");
			foreach (var info in typeof(TestStaticClass)
				.GetFields(BindingFlags.Static | BindingFlags.Public)
				.Where(f => f.FieldType == typeof(string)))
			{
				Console.WriteLine("{0}: '{1}'", info.Name, info.GetValue(null));
				
				info.SetValue(null, info.Name);
				
				Console.WriteLine("{0}: '{1}'", info.Name, info.GetValue(null));
			}
		}
	}

	[TestFixture]
	public class ReflectTests
	{
		[Test]
		public void Instance_Field()
		{
			Reflect<Class>.Field(c => c.Field).Name.Should().Be.EqualTo("Field");
		}

		[Test]
		public void Instance_MemberName()
		{
			Reflect<Class>.MemberName(c => c.Field).Should().Be.EqualTo("Field");
		}

		[Test]
		public void StaticField_ConstructorInitialization()
		{
			TestStaticClass.String1.Should().Be.EqualTo(Reflect.Name(() => TestStaticClass.String1));
			TestStaticClass.String2.Should().Be.EqualTo(Reflect.Name(() => TestStaticClass.String2));
		}
	}
}
