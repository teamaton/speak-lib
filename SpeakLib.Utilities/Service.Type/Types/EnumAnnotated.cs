using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class EnumAnnotated
    {
        public string Name;
        public Enum Enum;

        /// <summary>
        /// The integer value of the enum
        /// </summary>
        public int Value { get{ return Convert.ToInt32(Enum); } }

        public EnumAnnotated(){}

        public EnumAnnotated(string name, object value)
        {
            Name = name;
            Enum = (Enum) value;
        }

    }
}
