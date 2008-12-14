using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.FileUpload
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FileSelect runat=server></{0}:FileSelect>")]
    public class FileSelect : CompositeControl
    {
        private HtmlInputFile inputFile;

        private string onSelected;
        internal string OnSelected
        {
            get { return onSelected; }
            set { onSelected = value; RecreateChildControls(); }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
           
            inputFile = new HtmlInputFile {ID = "inputFile"};
            inputFile.Attributes["onchange"] = OnSelected;
            Controls.Add(inputFile);
        }

    }
}
