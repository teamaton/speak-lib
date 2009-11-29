using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NUnit.Framework;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Web;

namespace Tests.Utilities.Web
{
    [TestFixture]
    public class StringExtensionsTest : AssertionHelper
    {
        [Test]
        public void LineWrap()
        {
            Console.WriteLine("---1---");
            var text =
                "Das Adriatic Kamp ist ein sehr kleiner Campingplatz mit ungefähr 50 Stellplätzen. Man sollte daher unbedingt reservieren, wenn man die Absicht hat, dort zu campen. Unser Kamp liegt am südlichen Hang der Halbinsel Peljesac, 4 Km östlich von Orebic, dem alten Kapitänsnest, gegenüber der Insel Korcula mit der gleichnamigen Stadt- dem Geburtsort von Marco Polo. Im Schatten alter Olivenbäumen und Pinien, unweit vom wunderschönen Kiesstrand und kristallklarem Meer befindet sich Camp sowie Appartements, Restaurant, Tauchbasis, Beach-Bar und Fittness “Adriatic” der Familie Mikulić.";

            var wrapped = text.WordWrap(80);
            wrapped.ForEach(Console.WriteLine);

            wrapped.ForEach(line => Assert.LessOrEqual(line.Count(), 80));

            Console.WriteLine("---2---");
            var wrappedText = text.Wrap(80);
            Console.WriteLine(wrappedText);

            Console.WriteLine("---3---");
            text =
@"Das Adriatic Kamp ist ein sehr 
kleiner Campingplatz mit ungefähr 50 Stellplätzen. Man sollte daher unbedingt reservieren,
wenn man die Absicht hat, dort zu campen.
Unser Kamp liegt am südlichen Hang der Halbinsel Peljesac, 
4 Km östlich von Orebic, dem alten Kapitänsnest, gegenüber der Insel Korcula
mit der gleichnamigen Stadt- dem Geburtsort von Marco Polo. Im Schatten alter Olivenbäumen und Pinien, 
unweit vom 
wunderschönen Kiesstrand und kristallklarem Meer befindet sich Camp sowie Appartements, Restaurant, Tauchbasis, Beach-Bar und Fittness “Adriatic” der Familie Mikulić.";

            wrapped = text.WordWrap(80);
            wrapped.ForEach(Console.WriteLine);

            wrapped.ForEach(line => Assert.LessOrEqual(line.Count(), 80));
        }

        [Test]
        public void IsNumeric()
        {
            Expect("0".IsNumeric());
            Expect("5".IsNumeric());
            Expect("23".IsNumeric());
            Expect("-17".IsNumeric());

            Expect(!"".IsNumeric());
            Expect(!"-".IsNumeric());
            Expect(!"A".IsNumeric());
            Expect(!"23B".IsNumeric());
        }
    }
}
