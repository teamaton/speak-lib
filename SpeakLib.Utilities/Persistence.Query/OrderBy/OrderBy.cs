using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class OrderBy
    {
        private readonly OrderByCriteria _criteria;
        private readonly string _propertyName;
        private OrderDirection _direction = OrderDirection.Ascending;

        public string PropertyName { get { return _propertyName; } }
        public OrderDirection Direction { get{ return _direction; } }

        public OrderBy(string propertyName, OrderByCriteria criteria)
        {
            _criteria = criteria;
            _propertyName = propertyName;
        }

        public void Asc()
        {
            _direction = OrderDirection.Ascending;
            _criteria.Current = this;
        }

        public void Desc()
        {
            _direction = OrderDirection.Descending;
            _criteria.Current = this;
        }

        public void Set(OrderDirection direction)
        {
            _direction = direction;
            _criteria.Current = this;
        }

        public void Toggle()
        {
            if (_direction == OrderDirection.Descending)
                _direction = OrderDirection.Ascending;
            else
                _direction = OrderDirection.Descending;

            _criteria.Current = this;
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

    }
}
