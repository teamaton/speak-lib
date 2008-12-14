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
    [ToolboxData("<{0}:FileUploadContent runat=server></{0}:FileUploadContent>")]
    public class FileUploadContent : CompositeControl
    {
        private Panel selectPanel;
        private Panel loadingPanel;

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                var sessionUpload = new SessionUpload(UploadManagerId);
                var uploadManager = sessionUpload.GetUploadManager();
            }
        }

        private string UploadFileFunctionName
        {
            get { return string.Format("{0}_uploadFile()", ClientID); }
        }

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        DefaultValue(typeof(ITemplate), ""),
        Description("Control template"),
        TemplateContainer(typeof(FileUploadContent))
        ]
        public virtual ITemplate SelectTemplate { get; set; }
        sealed class DefaultSelectTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control owner)
            {
                var fileSelect = new FileSelect();
                owner.Controls.Add(fileSelect);
            }
        }

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        DefaultValue(typeof(ITemplate), ""),
        Description("Control template"),
        TemplateContainer(typeof(FileUploadContent))
        ]
        public virtual ITemplate LoadingTemplate { get; set; }
        sealed class DefaultLoadingTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control owner)
            {
                var message = new Label {Text = "Uploading..."};
                owner.Controls.Add(message);
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            selectPanel = new Panel { ID = "selectPanel" };
            if (SelectTemplate == null)
                SelectTemplate = new DefaultSelectTemplate();
            SelectTemplate.InstantiateIn(selectPanel);
            var fileSelects = selectPanel.Controls.OfType<FileSelect>();
            if (fileSelects.Count() != 1)
                throw new InvalidOperationException(
                    "The SelectTemplate must contain exactly one FileSelect control.");
            fileSelects.First().OnSelected = UploadFileFunctionName;
            Controls.Add(selectPanel);

            loadingPanel = new Panel { ID = "loadingPanel" };
            loadingPanel.Style["display"] = "none";
            if (LoadingTemplate == null)
                LoadingTemplate = new DefaultLoadingTemplate();
            LoadingTemplate.InstantiateIn(loadingPanel);
            Controls.Add(loadingPanel);
        }

        public string UploadManagerId
        {
            get
            {
                return Page.Request["sf_uploadManagerId"];
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            var script = string.Format(
                @"
                function {0} {{
                    $get('{1}').style.display = 'none';
                    $get('{2}').style.display = '';
                    setTimeout('document.{3}.submit()', 1000); 
                }}",
                UploadFileFunctionName, 
                selectPanel.ClientID, 
                loadingPanel.ClientID, 
                Page.Form.Name);

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "UploadFileFunctionName",
                                                        script, true);
        }
    }
}
