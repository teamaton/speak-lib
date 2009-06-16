using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities;

namespace Tests.Utilities.Query
{
    [TestFixture]
    public class PagerBehaviourTest : AssertionHelper
    {
        private IPager _pager;

        [SetUp]
        public void SetUp()
        {
            _pager = new Pager {TotalItems = 1337, PageSize = 10};
        }
        
        [Test]
        public void PageCount()
        {
            Expect(_pager.PageCount, EqualTo(134));
        }

        [Test]
        public void ChangePage()
        {
            Expect(_pager.CurrentPage,EqualTo(1));
            
            _pager.NextPage();
            Expect(_pager.CurrentPage, EqualTo(2));

            _pager.NextPage(23);
            Expect(_pager.CurrentPage, EqualTo(25));

            _pager.PreviousPage();
            Expect(_pager.CurrentPage, EqualTo(24));

            _pager.PreviousPage(14);
            Expect(_pager.CurrentPage, EqualTo(10));

            _pager.LastPage();
            Expect(_pager.CurrentPage, EqualTo(134));

            _pager.FirstPage();
            Expect(_pager.CurrentPage, EqualTo(1));
        }

        [Test]
        public void Constraints()
        {
            _pager.CurrentPage = -12;
            Expect(_pager.CurrentPage, EqualTo(1));

            _pager.NextPage(14);
            Expect(_pager.CurrentPage, EqualTo(15));

            _pager.CurrentPage = 144;
            Expect(_pager.CurrentPage, EqualTo(134));

            _pager.TotalItems = 2300;
            Expect(_pager.CurrentPage, EqualTo(144));
        }

        [Test]
        public void Bounds()
        {
            _pager.CurrentPage = 25;
            Expect(_pager.LowerBound, EqualTo(241));
            Expect(_pager.UpperBound, EqualTo(250));
            Expect(_pager.NextLowerBound, EqualTo(251));
            Expect(_pager.NextUpperBound, EqualTo(260));

            _pager.LastPage();
            Expect(_pager.LowerBound, EqualTo(1331));
            Expect(_pager.UpperBound, EqualTo(1337));
            Expect(_pager.NextLowerBound, EqualTo(1331));
            Expect(_pager.NextUpperBound, EqualTo(1337));
        }

        [Test]
        public void FirstResult()
        {
            _pager.CurrentPage = 25;
            Expect(_pager.FirstResult, EqualTo(240));

            _pager.FirstPage();
            Expect(_pager.FirstResult, EqualTo(0));
            
            _pager.LastPage();
            Expect(_pager.FirstResult, EqualTo(1330));

            _pager.CurrentPage = 999;
            Expect(_pager.FirstResult, EqualTo(1330));
        }
       
    }
}
