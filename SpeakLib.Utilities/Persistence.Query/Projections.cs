using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.SqlCommand;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	public class _Projections
	{
		private _Projections()
		{
		}

		public static IProjection DoubleAvg(string propertyName)
		{
			return Projections.ProjectionList()
				.Add(Projections.SqlFunction(new ClassicAvgFunction(),
				                             NHibernateUtil.Double,
				                             Projections.Cast(NHibernateUtil.Double,
				                                              Projections.Property(propertyName))));
		}

		public static IProjection EmptyGroupDoubleAvg(string propertyName)
		{
			return Projections.ProjectionList()
				.Add(Projections.SqlFunction(new ClassicAvgFunction(),
				                             NHibernateUtil.Double,
				                             Projections.Cast(NHibernateUtil.Double,
				                                              EmptyGroupProperty(propertyName))));
		}

		private static IProjection EmptyGroupProperty(string propertyName)
		{
			return new EmptyGroupPropertyProjection(propertyName);
		}

		/// <summary>
		/// Robert(2008-05-10): Not used anyware, so does when does it make to use the method?
		/// </summary>
		private class EmptyGroupPropertyProjection : PropertyProjection
		{
			public EmptyGroupPropertyProjection(string propertyName) : base(propertyName, true)
			{
			}

			public override SqlString ToGroupSqlString(
				ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
			{
				return new SqlString("");
			}
		}
	}
}