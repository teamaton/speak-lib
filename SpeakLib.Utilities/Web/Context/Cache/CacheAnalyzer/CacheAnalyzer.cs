using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web.Analysis
{
    public class CacheAnalyzer
    {
        public CacheItemTypeSummaryList GetUniqueTypes()
        {
            var enumerator = Cache.GetEnumerator();
            var result = new CacheItemTypeSummaryList();

            while(enumerator.MoveNext()){
                result.Add(enumerator.Entry);
            }

            return result;
        }

    }
}
