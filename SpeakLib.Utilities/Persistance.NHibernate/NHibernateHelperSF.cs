using System;
using System.IO;
using System.Linq;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Stat;
using NHibernate.Tool.hbm2ddl;

namespace SpeakFriend.Utilities
{
	public class NHibernateHelperSF
	{ 
		internal ISession _session;
		protected static ILog Logger = LogManager.GetLogger(typeof (NHibernateHelperSF));
		private Configuration _cfg;

		public NHibernateHelperSF(ISession session)
		{
			_session = session;
		}

		public ISession Session { get { return _session; } }

		public void ExecuteFile(string filePath)
		{
			string sql = File.ReadAllText(filePath);

			if (sql.IndexOf("COMMIT", StringComparison.InvariantCultureIgnoreCase) > 0)
			{
				var transactions = sql.Split(new[] {"COMMIT"},
				                             StringSplitOptions.RemoveEmptyEntries)
					.ToList().ConvertAll(t => t + " COMMIT");

				transactions.ForEach(t =>
				                     	{
				                     		_session.CreateSQLQuery(t).ExecuteUpdate();
				                     		_session.Flush();
				                     	});
			}
			else
			{
				_session.CreateSQLQuery(sql).ExecuteUpdate();
				_session.Flush();
			}
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

		public static void ExportSchema(Configuration cfg)
		{
			var schemaUpdate = new SchemaExport(cfg);
			schemaUpdate.Execute(true, true, true);
		}

		public void ExportSchema()
		{
			_cfg = new Configuration();
			ExportSchema(_cfg);
		}
	}
}