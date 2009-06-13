using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class SearchDescriptionBase<TFilter, TOrderBy> : Pager, ISearchDesc
        where TFilter : ConditionContainer, new()
        where TOrderBy : OrderByCriteria, new()
    {
        ConditionContainer ISearchDesc.Filter { get { return Filter; } }
        OrderByCriteria ISearchDesc.OrderBy { get { return OrderBy; } }

        private readonly TFilter _filter = new TFilter();
        private readonly TOrderBy _orderBy = new TOrderBy();

        public TFilter Filter { get { return _filter; } }
        public TOrderBy OrderBy { get { return _orderBy; } }
    }
}
