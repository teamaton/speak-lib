using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Stat;

namespace SpeakFriend.Utilities.Persistance
{
    public class NHibernateLogger
    {
        public static void PrintLoadStatistics(IStatistics statistics)
        {
            Console.WriteLine();
            Console.WriteLine("== Short Summary at: {0} ==", DateTime.Now);
            Console.WriteLine("CollectionFetchCount: {0}", statistics.CollectionFetchCount);
            Console.WriteLine("CollectionLoadCount: {0}", statistics.CollectionLoadCount);

            Console.WriteLine("EntityFetchCount: {0}", statistics.EntityFetchCount);
            Console.WriteLine("EntityLoadCount: {0}", statistics.EntityLoadCount);

            Console.WriteLine("FlushCount: {0}", statistics.FlushCount);
        }

        public static void PrintSessionStatistics(ISessionStatistics statistics)
        {
            Console.WriteLine();
            Console.WriteLine("== Short Session Statistics Summary at: {0} ==", DateTime.Now);
            Console.WriteLine("CollectionCount: {0}", statistics.CollectionCount);
            if (statistics.CollectionCount > 0)
            {
                Console.WriteLine("CollectionKeys:");
                statistics.CollectionKeys.ToList().ForEach(
                    key => Console.Write("  ->  " + key.Key + " (Role: " + key.Role + ")"));
                Console.WriteLine();
            }
            Console.WriteLine("EntityCount: {0}", statistics.EntityCount);
            if (statistics.EntityCount > 0)
            {
                Console.WriteLine("EntityKeys:");
                statistics.EntityKeys.ToList().ForEach(
                    key => Console.Write("  ->  " + key.EntityName + " (Id: " + key.Identifier + ")"));
            }
            Console.WriteLine();
        }

        public static void PrintSessionStatistics(ISession session)
        {
            Console.WriteLine();
            Console.WriteLine("== Current Session Hashcode: {0} ==", session.GetHashCode());
            PrintSessionStatistics(session.Statistics);
        }
    }
}
