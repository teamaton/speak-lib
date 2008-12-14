using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: TagPrefix("SpeakFriend.FileUpload", "speakFriend")]
namespace SpeakFriend.FileUpload
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FileUploadFrame runat=server></{0}:FileUploadFrame>")]
    public class FileUploadFrame : WebControl
    {
        private readonly HtmlGenericControl iframe;
        private readonly SessionUpload sessionUpload;

        public string UploadManagerId
        {
            get
            {
                if (string.IsNullOrEmpty(ViewState["uploadManagerId"] as string))
                    ViewState["uploadManagerId"] = Guid.NewGuid().ToString();
                return ViewState["uploadManagerId"] as string;
            }
            set { ViewState["uploadManagerId"] = value; }
        }

        public string ContentUrl { get; set; }

        public FileUploadFrame()
        {
            iframe = new HtmlGenericControl("iframe");
            sessionUpload = new SessionUpload(UploadManagerId);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            iframe.Attributes["src"] = string.Format("{0}?sf_uploadManagerId={1}", ContentUrl, UploadManagerId);
            iframe.RenderControl(output);
        }
    }
}
