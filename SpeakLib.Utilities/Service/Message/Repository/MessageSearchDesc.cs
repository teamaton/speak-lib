using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class MessageSearchDesc : Pager, ICloneable, ISearchDesc
    {
        private MessageSearchFilter _filter;
        public MessageSearchFilter Filter{ get { return _filter ?? (_filter = new MessageSearchFilter()); } }
        
        private readonly MessageOrderBy _orderBy = new MessageOrderBy();
        public MessageOrderBy OrderBy { get { return _orderBy; } }

		ConditionContainer ISearchDesc.Filter
		{
			get { return Filter; }
		}

		OrderByCriteria ISearchDesc.OrderBy
		{
			get { return OrderBy; }
		}

		public MessageSearchDesc()
		{
			PageSize = 10;
		}

        public object Clone()
        {
        	var newSearchDesc = new MessageSearchDesc
        	                    	{
        	                    		_filter = Filter,
        	                    		PageSize = PageSize,
        	                    		_isInSingleItemMode = _isInSingleItemMode,
        	                    		CurrentPage = CurrentPage,
        	                    		_navigationPagerResultIndex = _navigationPagerResultIndex,
        	                    		QueryAll = QueryAll,
        	                    		QueryExpiredRequests = QueryExpiredRequests,
        	                    		TotalItems = TotalItems
        	                    	};
            newSearchDesc._orderBy.Current = OrderBy.Current;
            return newSearchDesc;
        }
    }
}
