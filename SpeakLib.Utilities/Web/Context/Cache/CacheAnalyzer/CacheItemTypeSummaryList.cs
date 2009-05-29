using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.ValueObjects;

namespace SpeakFriend.Utilities.Web.Analysis
{
    /// <summary>
    /// The Cacheitem
    /// </summary>
    public class CacheItemTypeSummaryList : List<CacheItemTypeSummary>
    {
        public void Add(DictionaryEntry summary)
        {
            var wrapper = new CacheItemWrapper(summary);
            Add(wrapper.ToCacheItemSummary());
        }

        public new void Add(CacheItemTypeSummary typeSummary)
        {
            var item = GetByType(typeSummary.Type);

            if (item == null)
                base.Add(typeSummary);
            else
            {
                item.Amount += typeSummary.Amount;
                item.Size += typeSummary.Size;
            }
                
        }

        /// <summary>
        /// Returns null if Summary for the given type is not found
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public CacheItemTypeSummary GetByType(Type type)
        {
            return Find(item => item.Type == type);
        }

        public bool ContainsType(Type type)
        {
            return GetByType(type) != null;
        }

        /// <summary>
        /// The total size, in case of serilization
        /// </summary>
        public BinarySize TotalSize
        {
            get
            {
                var result = new BinarySize();
                foreach (var summary in this)
                    result.Bytes =+ summary.Size.Bytes;

                return result;
            }
        }
    }
}
