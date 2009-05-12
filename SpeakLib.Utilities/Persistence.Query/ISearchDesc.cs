using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public interface ISearchDesc : IPager
    {
        ConditionContainer GetFilter();
        OrderByCriteria GetOrderBy();
    }
}
