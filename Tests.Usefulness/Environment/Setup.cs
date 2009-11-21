using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness.Environment
{
    class Setup : BaseTest
    {
		public static void InitializeAll()
		{
			
		}

        public static void CleanUp()
        {
            foreach (var type in new[] { typeof(UsefulEntity), typeof(UsefulnessEntry)})
                _nHibernateHelper.EmptyTable(type);
        }

        public static void CreateTestEnvironment()
        {
            CleanUp();

            //some random objects
        	for (int i = 0;
        	     i < 3;
        	     i++)
        	{
        		
        	}

        	//some possible tags
            foreach (var text in new[] { "Food", "Yummy", "Expensive", "I need it!", "Liquid" })
            {
            }

        }
    }
}
