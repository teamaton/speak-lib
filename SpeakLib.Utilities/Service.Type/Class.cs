using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SpeakFriend.Utilities
{
    public static class Class
    {
        /// <summary>
        /// Clones all serializiable members of class, 
        /// the class must be serializable ([Serializable]).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-an-object-in-net-c-specifically
        /// </remarks>
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

		public static T Clone<T>(this T other) where T:class 
		{
			Type type = other.GetType();
			PropertyInfo[] properties = type.GetProperties();
			T retObject = (T)type.InvokeMember("", BindingFlags.CreateInstance, null, other, null);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanWrite)
				{
					propertyInfo.SetValue(retObject, propertyInfo.GetValue(other, null), null);
				}
			}
			return retObject;

		} 
    }
}
