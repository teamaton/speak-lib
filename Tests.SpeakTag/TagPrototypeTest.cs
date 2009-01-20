using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Tagging;
using SpeakTag.Tests.Environment;

namespace SpeakTag.Tests
{
    [TestFixture]
    public class TagPrototypeTest
    {
        private TagPrototypeService _prototypeService;

        [SetUp]
        public void SetUp()
        {
            Setup.CleanUp();
        }

        [Test]
        public void CreateAndGet()
        {
            var testPrototype = new TagPrototype(typeof (TestTarget)) {Text = "Test"};
            _prototypeService.Create(testPrototype);

            var items = _prototypeService.GetByTargetType(typeof(TestTarget));
            Assert.AreEqual(1, items.Count);

            testPrototype = items[0];

            Assert.AreEqual("Test", testPrototype.Text);
            Assert.AreEqual(typeof (TestTarget), testPrototype.TargetType);
        }

        [Test]
        public void TestEnvironmentPrototypes()
        {
            Setup.CreateTestEnvironment();
            
            var protos = _prototypeService.GetByTargetType(typeof(TestTarget));
            Assert.AreEqual(5, protos.Count);
        }

    }
}
