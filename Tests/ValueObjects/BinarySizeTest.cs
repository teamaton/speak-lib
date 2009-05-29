using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.ValueObjects;

namespace Tests.ValueObjects
{
    [TestFixture]
    public class BinarySizeTest
    {

        [Test]
        public void Formating()
        {
            var binarySizeTest = new BinarySize(1501);
            Console.Out.WriteLine(binarySizeTest.Formatted);
        }

        [Test]
        public void Addion()
        {
            var binarySizeA = new BinarySize(600);
            var binarySizeB = new BinarySize(600);

            Assert.That(binarySizeA + binarySizeB, Is.EqualTo(new BinarySize(1200)));     
        }
    }
}
