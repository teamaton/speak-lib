using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public interface IOrderByCriteria
    {
        OrderBy Current { get; }
        void Unset();
        bool IsSet();
    }

    public class OrderByCriteria : IOrderByCriteria
    {
		internal bool IsAdding { get; set; }

    	public OrderBy Current
    	{
			get { return CurrentList.Count > 0 ? CurrentList[0] : null; }
			set
			{
				Unset();
				if (value != null)
					Add(value);
			}
    	}

		public OrderByList CurrentList { get; private set; }

		public OrderByCriteria()
		{
			CurrentList = new OrderByList();
		}

    	internal void BeginAdding()
    	{
    		IsAdding = true;
    	}

    	internal void EndAdding()
    	{
    		IsAdding = false;
    	}

		public void Add(OrderBy orderBy)
		{
			CurrentList.RemoveAll(order => order.PropertyName == orderBy.PropertyName);
			CurrentList.Add(orderBy);
		}

    	public bool IsSet()
    	{
    		return CurrentList.Count > 0;
    	}

    	public void Unset()
        {
            CurrentList.Clear();
		}

		public T AndOrderBy<T>() where T : OrderByCriteria
		{
			return Current.AndOrderBy<T>();
		}
    }
}
