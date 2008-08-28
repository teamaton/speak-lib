using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SpeakFriend.Web.Utils;

namespace Frontend.Web
{
    public partial class MultiUpload : System.Web.UI.UserControl
    {
        private readonly List<HtmlTableRow> uploadRows = new List<HtmlTableRow>();
        private readonly List<HtmlGenericControl> iframes = new List<HtmlGenericControl>();
        private int _defaultMaxCount = 5;

        public string UploadManagerId { get; set; }
        public int MaximumImageCount
        {
            get { return _defaultMaxCount; }
            set { _defaultMaxCount = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rptUploadRows.ItemDataBound += rptUploadRows_ItemDataBound;

            if(!IsPostBack)
            {
                var dataSource = new List<int>();
                for (int i = 0; i < MaximumImageCount; i++)
                    dataSource.Add(i);

                rptUploadRows.DataSource = dataSource;
                rptUploadRows.DataBind();
            }

//            trAddRowRow.Style[HtmlTextWriterStyle.Display] = MaximumImageCount <= 1 ? "none" : "";
        }

        void rptUploadRows_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!ItemTemplateHelper.IsContentItem(e.Item))
                return;
            
            var index = (int)e.Item.DataItem;
            var helper = new ItemTemplateHelper(e.Item);

            var row = helper.Find<HtmlTableRow>("trUploadRow");

            if (index == 0)
                row.Style[HtmlTextWriterStyle.Display] = "";
            else
                row.Style[HtmlTextWriterStyle.Display] = "none";

            uploadRows.Add(row);

            var aRemoveRow = helper.Find<HtmlAnchor>("aRemoveRow");
            aRemoveRow.HRef = string.Format("javascript:RemoveUploadRow({0})", index);

            var iframe = helper.Find<HtmlGenericControl>("imageUpload");
            iframe.Attributes["src"] += string.Format("?oldId=-1&Id={0}&umId={1}", index, UploadManagerId);
            iframes.Add(iframe);
        }

        public string GetUploadRowIds()
        {
            var result = new StringBuilder();
            var first = true;
            foreach (var uploadRow in uploadRows)
            {
                if (!first) result.Append(',');
                else first = false;
                result.Append("'" + uploadRow.ClientID + "'");
            }
            return result.ToString();
        }

        public string GetIframeIds()
        {
            var result = new StringBuilder();
            var first = true;
            foreach (var iframe in iframes)
            {
                if (!first) result.Append(',');
                else first = false;
                result.Append("'" + iframe.ClientID + "'");
            }
            return result.ToString();
        }

        public List<TextBox> GetDescriptionBoxes()
        {
            return
            new List<RepeaterItem>(rptUploadRows.Items.Cast<RepeaterItem>()).ConvertAll(
                item => new ItemTemplateHelper(item).Find<TextBox>("txDescription"));
        }
    }
}