using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakTag.Tests._Code;
using SpeakTag.Tests.Environment;

namespace SpeakTag.Tests
{
    [TestFixture]
    public class TagTest : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TagCount()
        {
            Setup.CreateTestEnvironment();

            Setup.TagObjects();

            Assert.AreEqual(2, _tagService.GetByItem(_targetService.GetByName("Apple")).Count);
            Assert.AreEqual(3, _tagService.GetByItem(_targetService.GetByName("Apple Juice")).Count);
            Assert.AreEqual(2, _tagService.GetByItem(_targetService.GetByName("Banana")).Count);
            Assert.AreEqual(1, _tagService.GetByItem(_targetService.GetByName("Raw Potato")).Count);
            Assert.AreEqual(1, _tagService.GetByItem(_targetService.GetByName("Car")).Count);
            Assert.AreEqual(4, _tagService.GetByItem(_targetService.GetByName("Coffee")).Count);
        }
    }
}
