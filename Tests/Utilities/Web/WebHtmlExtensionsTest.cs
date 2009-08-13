using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NUnit.Framework;
using SpeakFriend.Utilities.Web;

namespace Tests.Utilities.Web
{
    [TestFixture]
    public class WebHtmlExtensionsTest
    {
        [Test]
        public void CssClassLabel()
        {
            var label = new Label();

            label.AddCssClass("first");
            Assert.IsTrue(label.CssClass.Equals("first"));

            label.AddCssClass("second-trick");
            Assert.IsTrue(label.CssClass.StartsWith("first") && label.CssClass.EndsWith("second-trick"));

            label.AddCssClass("second");
            Assert.AreEqual(label.CssClass, "first second-trick second");
        }

        [Test]
        public void CssClassHtml()
        {
            var htmlControl = new HtmlTableRow();

            htmlControl.AddCssClass("first");
            Assert.AreEqual("first", htmlControl.Attributes["class"]);

            htmlControl.AddCssClass("second-trick");
            Assert.AreEqual("first second-trick", htmlControl.Attributes["class"]);

            htmlControl.AddCssClass("second");
            Assert.AreEqual("first second-trick second", htmlControl.Attributes["class"]);

			htmlControl.SetCssClass("second", "third");
			Assert.AreEqual("second third", htmlControl.Attributes["class"]);

			htmlControl.ResetCssClass();
			Assert.AreEqual(string.Empty, htmlControl.Attributes["class"]);
        }
    }
}
