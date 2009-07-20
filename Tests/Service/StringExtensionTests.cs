using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities;

namespace Tests.Service
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ConvertToInt()
        {
            "22".ToInt32().Should().Be.EqualTo(22);

            (new Action(() => "abc".ToInt32()))
                .Should().Throw<FormatException>();

        }
    }
}
