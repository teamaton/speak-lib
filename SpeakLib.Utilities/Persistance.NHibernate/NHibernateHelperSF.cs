using System;
using System.IO;
using log4net;
using NHibernate;
using NHibernate.Stat;

namespace SpeakFriend.Utilities
{
	public class NHibernateHelperSF
	{ 
		internal ISession _session;
		protected static ILog Logger = LogManager.GetLogger(typeof (NHibernateHelperSF));

		public NHibernateHelperSF(ISession session)
		{
			_session = session;
		}

		public ISession Session { get { return _session; } }

		public void ExecuteFile(string filePath)
		{
			string sql = File.ReadAllText(filePath);

			_session.CreateSQLQuery(sql).ExecuteUpdate();
			_session.Flush();

			Console.WriteLine("### SQL ### Executed file '{0}'.", filePath);
		}

		public void EmptyTable(params Type[] types)
		{
			foreach (Type type in types)
				EmptyTable(type);
		}

		public void EmptyTable(Type type)
		{
			EmptyTable(type.Name);
		}

		public void TruncateTable(string tableName)
		{
			_session.CreateSQLQuery(String.Format("TRUNCATE TABLE {0}", tableName))
				.ExecuteUpdate();

			_session.Flush();

			if (Logger.IsDebugEnabled)
				Logger.Debug("Clearing session");

			_session.Clear();
		}

		public void EmptyTable(string typeName)
		{
			string queryFmt = "FROM {0}";
			string query = String.Format(queryFmt, typeName);

			_session.Delete(query);
			_session.Flush();

			if (Logger.IsDebugEnabled)
				Logger.Debug("Clearing session");

			_session.Clear();
		}

		public void Flush()
		{
			_session.Flush();
		}

		/// <summary>
		/// Clear the currently open session.
		/// </summary>
		public void Clear()
		{
			_session.Clear();
		}

		public void RemoveFromCache(object item)
		{
			_session.Evict(item);
		}

		public ISessionStatistics GetStatisticsSession()
		{
			return _session.Statistics;
		}

		public IStatistics GetStatistics()
		{
			return _session.SessionFactory.Statistics;
		}
	}
}