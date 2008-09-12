using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SpeakFriend.TrueOrFalse.Tests.Domain.Question
{
    [TestFixture]
    public class True
    {
        public delegate void Greeter(string name);

        [Test]
        public void fooooo()
        {
            // Instantiate the delegate using an anonymous method
            Greeter dopppelt = delegate(string k) { Console.WriteLine(k); Console.WriteLine(k); };
            dopppelt("5");

            Greeter cons = fooTest;
        }

        public void fooTest(string a){Console.WriteLine(a);}

    }
}
