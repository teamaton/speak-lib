using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Web;
using SpeakFriend.Utilities.Web.Analysis;

namespace Tests.Utilities.Web.Context
{
    [TestFixture]
    public class CacheAnalyzerTest
    {

        [Test]
        public void Usage()
        {
            Cache.Add("key1", "word1");
            Cache.Add("key2", 35);
            Cache.Add("key3", "word3");
            Cache.Add("key4", new ArrayList());

            var cacheAnalyzer = new CacheAnalyzer();
            var typeSummeries = cacheAnalyzer.GetUniqueTypes();
            
            Assert.That(typeSummeries.Count, Is.EqualTo(3));
            Assert.That(typeSummeries.GetByType(typeof(ArrayList)), Is.Not.Null);
            Assert.That(typeSummeries.GetByType(typeof(Int32)), Is.Not.Null);
            Assert.That(typeSummeries.GetByType(typeof(String)), Is.Not.Null);
            Assert.That(typeSummeries.GetByType(typeof(String)).Amount, Is.EqualTo(2));
            //sanity check, result varies on platforms
            Assert.That(typeSummeries.TotalSize.Bytes, Is.InRange(50, 300)); 
        }

    }
}
