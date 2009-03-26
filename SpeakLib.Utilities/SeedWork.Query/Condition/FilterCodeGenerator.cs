using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// This class is a helper, to create 
    /// copy and paste code for the Filter
    /// </summary>
    public class FilterCodeGenerator
    {
        static readonly List<string> _membersToGenerate = new List<string>();
        static readonly List<string> _constructorStatementsToGenerate = new List<string>();

        public static void Start(Type type)
        {
            MemberInfo[] members = type.GetMembers();

            foreach(MemberInfo member in members)
            {
                foreach(object attribute in member.GetCustomAttributes(true))
                {
                    if(attribute.GetType() == typeof(FilterBooleanAttribute))
                        AddToGenerate(member, "ConditionBoolean");
                    
                    if (attribute.GetType() == typeof(FilterIntegerAttribute))
                        AddToGenerate(member, "ConditionInteger");

                    if (attribute.GetType() == typeof(FilterSingleAttribute))
                        AddToGenerate(member, "ConditionSingle");

                    if (attribute.GetType() == typeof(FilterDecimalAttribute))
                        AddToGenerate(member, "ConditionDecimal");
                }
            }
            foreach(string memberToGenerate in _membersToGenerate)
                Console.WriteLine(memberToGenerate);

            Console.WriteLine();
            Console.WriteLine();

            foreach (string statementToGenerate in _constructorStatementsToGenerate)
                Console.WriteLine(statementToGenerate);            
        }

        private static void AddToGenerate(MemberInfo member, string className)
        {
            _membersToGenerate.Add("public " + className + " " + member.Name + ";");
            _constructorStatementsToGenerate.Add(
                String.Format("{0} = new "+ className +"(this, \"{1}\");",member.Name, member.Name));
        }
    }
}
