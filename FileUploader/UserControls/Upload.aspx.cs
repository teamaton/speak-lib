using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SpeakFriend.FileUploader;


namespace Frontend.Web
{
    public partial class ImageUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionPageDetail = new SessionUpload();
            var uploadManager = sessionPageDetail.GetUploadManager(Request["umId"]);

            if(IsPostBack)
            {
                string imageName;
                string thumbName = "";

                Thread.Sleep(UploadSettings.DebugDelayInSeconds * 1000);
                uploadManager.ManageUploadedImage(
                    fileInput.PostedFile.InputStream,
                    Convert.ToInt32(Request["Id"]),
                    out imageName,
                    out thumbName);
                
                imgUploadedImage.ImageUrl = String.Format("{0}/{1}", uploadManager.TempPathRelative, thumbName);
                
                // Add spaces to file path so that it can be wrapped.
                lblFileName.Text = fileInput.PostedFile.FileName.Replace("\\","\\ ");

                panFileInput.Visible = false;
                panUploadedImage.Visible = true;
                lblMsg.Visible = false;
            }
            else
            {
                var oldId = Convert.ToInt32(Request["OldId"]);
                if(oldId > -1)
                {
                    uploadManager.DeleteImage(oldId);
                }
            }
        }

        

        /// <summary>
        /// http://www.aspnetresources.com/articles/CustomErrorPages.aspx
        /// </summary>
        /// <param name="e"></param>
//        protected override void OnError(EventArgs e)
//        {
//            base.OnError(e);
//            return;
//
//            // At this point we have information about the error
//            HttpContext ctx = HttpContext.Current;
//
//            Exception exception = ctx.Server.GetLastError();
//
//            if (exception is HttpException)
//            {
//                var httpEx = exception as HttpException;
//                if (500 == httpEx.GetHttpCode())
//                {
//                    // Most likely too large file
//                    lblMsg.Visible = true;
//                    lblMsg.Text = "Die Datei ist zu groß.";
//                    ctx.Server.ClearError();
////                    Server.Transfer(ctx.Request.Url.ToString(), true);
//                    Response.SuppressContent = false;
//                    Response.Redirect(ctx.Request.Url.ToString(), true);
//                    Response.Flush();
//                }
//            }
//            
//        }
    }
}
