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
        public OrderBy Current { get; set; }

        public void Unset()
        {
            Current = null;
        }

        public bool IsSet()
        {
            return Current != null;
        }
    }
}
