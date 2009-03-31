using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace SpeakFriend.Utilities
{
    public abstract class Condition
    {
        public string PropertyName { get; set; }
        private readonly ConditionContainer _conditions;
        public ConditionContainer Conditions
        {
            get { return _conditions; }
        }

        
        public Condition(ConditionContainer conditions)
        {
            _conditions = conditions;
        }

        public Condition(ConditionContainer conditions, string propertyName)
        {
            _conditions = conditions;
            PropertyName = propertyName;
        }
        
        /// <summary>
        /// Entfernt diese <see cref="Condition"/> aus der Liste.
        /// </summary>
		public void Remove()
        {
            if (_conditions.Contains(PropertyName))
                _conditions.Remove(this);
        }

        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;

            Condition condition = (Condition)obj;

            if (!PropertyName.Equals(condition.PropertyName)) return false;

            return true;
        }

        public void AddUnique(Condition condition)
        {
            Conditions.AddUnique(condition);
        }

        public void AddUnique()
        {
            AddUnique(this);
        }

        public abstract void AddToCriteria(ICriteria criteria);
        public abstract ICriterion GetCriterion();

        public virtual void Reset()
        {
            Conditions.Remove(this);
        }

        public virtual bool IsActive()
        {
            return _conditions.Contains(this);
        }
    }
}
