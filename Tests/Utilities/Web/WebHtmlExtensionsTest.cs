using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
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

		[Test]
		public void ChildControlsOfType()
		{
			var userControl = new UserControl {ID = "uc"};
			var panel = new Panel {ID = "pl"};
			var htmlUl = new HtmlGenericControl("ul") {ID = "ul"};
			var htmlLi = new HtmlGenericControl("li") {ID = "li"};
			htmlLi.Controls.Add(new CheckBox {ID = "ckb1"});
			htmlUl.Controls.Add(htmlLi);
			panel.Controls.Add(htmlUl);
			panel.Controls.Add(new CheckBox {ID = "ckb2"});
			userControl.Controls.Add(panel);

			var checkBoxes = userControl.Controls<CheckBox>();
			Assert.AreEqual(2, checkBoxes.Count());
			Console.Write("Found: ");
			checkBoxes.ToList().ForEach(ckb => Console.Write(ckb.ID + ", "));
		}

		[Test]
		public void ControlsOfTypes()
		{
			var controlTree = new PlaceHolder();
			controlTree.Controls.Add(new Literal(){ID="lt_ToTrans"});
			controlTree.Controls.Add(new Literal(){ID="ltNoTrans"});
			var plh = new PlaceHolder();
			plh.Controls.Add(new Label(){ID="lbl_ToTrans"});
			plh.Controls.Add(new Image(){ID="img_ToTrans"});
			controlTree.Controls.Add(plh);

			var result = controlTree.ControlsOfType(new List<Type>() {typeof (Literal), typeof (Label), typeof (Image)});

			Assert.That(result.Count(), Is.EqualTo(4));
		}
    }
}
