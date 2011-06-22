using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class OrderByExtender
	{
		private readonly OrderByCriteria _andOrderByCriteria;

		public T AndOrderBy<T>() where T : OrderByCriteria
		{
			_andOrderByCriteria.BeginAdding();
			return (T)_andOrderByCriteria;
		}

		//		public OrderByCriteria AndOrderBy
		//		{
		//			get
		//			{
		//				_andOrderBy.BeginAdding();
		//				return _andOrderBy;
		//			}
		//			set { _andOrderBy = value; }
		//		}

		public OrderByExtender(OrderByCriteria orderByCriteria)
		{
			_andOrderByCriteria = orderByCriteria;
		}
	}

	[Serializable]
	public class OrderByExtenderT<T> where T : OrderByCriteria
	{
		private T _andOrderBy;

		public T AndOrderBy
		{
			get
			{
				_andOrderBy.BeginAdding();
				return _andOrderBy;
			}
			set { _andOrderBy = value; }
		}

		public OrderByExtenderT(OrderByCriteria orderBy)
		{
			AndOrderBy = (T)orderBy;
		}
	}

	[Serializable]
	public class OrderBy
	{
		private readonly OrderByCriteria _criteria;
		private readonly OrderByExtender _andOrderBy;
		private OrderDirection _direction = OrderDirection.Ascending;

		/// <summary>The table alias used in associations.</summary>
		public string Alias { get; private set; }

		public bool HasAlias
		{
			get { return !string.IsNullOrEmpty(Alias); }
		}

		/// <summary>
		/// An action to perform before adding the OrderBy to the Criteria 
		/// (e.g. CreateAlias) with this instance's <see cref="Alias"/>.
		/// </summary>
		public Action<ICriteria> CriteriaAction { get; private set; }

		public bool HasCriteriaAction
		{
			get { return CriteriaAction != null; }
		}

		public string PropertyName { get; private set; }

		public OrderDirection Direction
		{
			get { return _direction; }
		}

		public bool HasSqlFormula
		{
			get { return SqlFormula.IsNotNullOrEmpty(); }
		}

		public string SqlFormula { get; private set; }
		
		public OrderBy(string propertyName, OrderByCriteria criteria)
		{
			_criteria = criteria;
			PropertyName = propertyName;
			_andOrderBy = new OrderByExtender(criteria);
		}

		public OrderBy(string propertyName, OrderByCriteria criteria, string sqlFormula)
		{
			PropertyName = propertyName;
			_criteria = criteria;
			SqlFormula = sqlFormula;
		}

		public OrderBy(string propertyName, OrderByCriteria criteria, string alias, Action<ICriteria> criteriaAction)
			: this(propertyName, criteria)
		{
			Alias = alias;
			CriteriaAction = criteriaAction;
		}

		public OrderByExtender Asc()
		{
			return Set(OrderDirection.Ascending);
		}

		public OrderByExtender Desc()
		{
			return Set(OrderDirection.Descending);
		}

		/// <summary>
		/// Insert OrderBy to beginning of OrderBy list. Remove first if already contained.
		/// </summary>
		/// <returns></returns>
		public OrderBy InsertDesc()
		{
			_direction = OrderDirection.Descending;
			_criteria.CurrentList.Remove(this);
			_criteria.CurrentList.Insert(0, this);
			return this;
		}

		public OrderByExtender Set(OrderDirection direction)
		{
			_direction = direction;

			if (_criteria.IsAdding)
				_criteria.Add(this);
			else
				_criteria.Current = this;
			_criteria.EndAdding();

			return _andOrderBy;
		}

		public OrderBy Toggle()
		{
			_direction = _direction == OrderDirection.Descending
			             	? OrderDirection.Ascending
			             	: OrderDirection.Descending;

			return this;
		}

		public void AscOrToggle()
		{
			if (!IsCurrent())
				Asc();
			else
				Toggle();
		}

		public bool IsDesc()
		{
			return _direction == OrderDirection.Descending;
		}


		public bool IsAsc()
		{
			return _direction == OrderDirection.Ascending;
		}

		public bool IsCurrent()
		{
			return _criteria.Current == this;
		}

		public T AndOrderBy<T>() where T : OrderByCriteria
		{
			return _andOrderBy.AndOrderBy<T>();
		}
	}

	[Serializable]
	public class OrderByList : List<OrderBy>
	{
	}

	/**
	 * Extends {@link org.hibernate.criterion.Order} to allow ordering by an SQL formula passed by the user.
	 * Is simply appends the <code>sqlFormula</code> passed by the user to the resulting SQL query, without any verification.
	 * @author Sorin Postelnicu
	 * @since Jun 10, 2008
	 */
	public class OrderBySqlFormula : Order
	{
		private readonly String _sqlFormula;

		/**
		 * Constructor for Order.
		 * @param _sqlFormula an SQL formula that will be appended to the resulting SQL query
		 */
		protected OrderBySqlFormula(String sqlFormula)
			: base(sqlFormula, true)
		{
			_sqlFormula = sqlFormula;
		}

		public override String ToString()
		{
			return _sqlFormula;
		}

		public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
		{
			return new SqlString(_sqlFormula);
		}

		/**
		 * Custom order
		 *
		 * @param sqlFormula an SQL formula that will be appended to the resulting SQL query
		 * @return Order
		 */
		public new static Order Asc(String sqlFormula)
		{
			return new OrderBySqlFormula(sqlFormula + " asc");
		}
		public new static Order Desc(String sqlFormula)
		{
			return new OrderBySqlFormula(sqlFormula + " desc");
		}
	}
}