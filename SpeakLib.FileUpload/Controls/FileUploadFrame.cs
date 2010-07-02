using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [ToolboxData("<{0}:FileUploadFrame runat=server></{0}:FileUploadFrame>")]
    public class FileUploadFrame : WebControl
    {
        private readonly HtmlGenericControl iframe;
        private SessionUpload SessionUpload { get { return new SessionUpload(UploadManagerId); } }


        protected override object SaveViewState()
        {
            EnsureUploadManagerId();
            return base.SaveViewState();
        }

        public string UploadManagerId
        {
            get
            {
                EnsureUploadManagerId();
                return ViewState["uploadManagerId"] as string;
            }
        }

        private void EnsureUploadManagerId()
        {
            if (string.IsNullOrEmpty(ViewState["uploadManagerId"] as string))
                ViewState["uploadManagerId"] = Guid.NewGuid().ToString();
        }

        public string ContentUrl { get; set; }

        public FileUploadFrame()
        {
            iframe = new HtmlGenericControl("iframe");
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            iframe.Attributes["src"] = string.Format("{0}?sf_uploadManagerId={1}", ContentUrl, UploadManagerId);
            iframe.Attributes["frameborder"] = "0";
            iframe.RenderControl(output);
        }

    	public IList<UploadedFile> UploadedFiles
    	{
    		get { return SessionUpload.UploadManager.UploadedFiles; }
    	}

        public void ClearFiles()
        {
            SessionUpload.UploadManager.Clear();
        }
    }
}
