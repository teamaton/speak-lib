using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.FileUpload
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FileUploadContent runat=server></{0}:FileUploadContent>")]
    public class FileUploadContent : CompositeControl
    {
        private Panel selectPanel;
        private Panel loadingPanel;
        private Panel uploadedPanel;

        private UploadedFile UploadedFile
        {
            get { return (UploadedFile) ViewState["uploadedFile"]; }
            set { ViewState["uploadedFile"] = value; }
        }
        private bool Uploaded { get; set; }

        private UploadManager UploadManager
        {
            get
            {
                return new SessionUpload(UploadManagerId).UploadManager;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();


            if (Page.IsPostBack)
            {
                var fileSelect = GetFileSelect(selectPanel);

                if (fileSelect.HasFile)
                {
                    UploadedFile = UploadManager.HandleFile(fileSelect.PostedFile);
                    Uploaded = true;
                    UpdateUploadedFileControls();
                }
            }

            SetView();
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

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        DefaultValue(typeof(ITemplate), ""),
        Description("Control template"),
        TemplateContainer(typeof(FileUploadContent))
        ]
        public virtual ITemplate UploadedTemplate { get; set; }
        sealed class DefaultUploadedTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control owner)
            {
                var name = new UploadedFileName();
                owner.Controls.Add(name);

                var delete = new UploadedFileDelete();
                owner.Controls.Add(delete);
            }
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (selectPanel == null)
            {
                selectPanel = new Panel {ID = "selectPanel"};
                if (SelectTemplate == null)
                    SelectTemplate = new DefaultSelectTemplate();
                SelectTemplate.InstantiateIn(selectPanel);
                FileSelect fileSelect = GetFileSelect(selectPanel);
                fileSelect.OnSelected = UploadFileFunctionName;
            }
            Controls.Add(selectPanel);

            if (loadingPanel == null)
            {
                loadingPanel = new Panel {ID = "loadingPanel"};

                loadingPanel.Style["display"] = "none";
                if (LoadingTemplate == null)
                    LoadingTemplate = new DefaultLoadingTemplate();
                LoadingTemplate.InstantiateIn(loadingPanel);
            }
            Controls.Add(loadingPanel);

            if (uploadedPanel == null)
            {
                uploadedPanel = new Panel {ID = "uploadedPanel"};
                if (UploadedTemplate == null)
                    UploadedTemplate = new DefaultUploadedTemplate();
                UploadedTemplate.InstantiateIn(uploadedPanel);
                UpdateUploadedFileControls();
            }
            Controls.Add(uploadedPanel);
        }

        private void UpdateUploadedFileControls()
        {
            foreach (var uploadedFileControl in uploadedPanel.Controls.OfType<UploadedFileControl>())
            {
                uploadedFileControl.UploadManager = UploadManager;
                uploadedFileControl.File = UploadedFile;
            }
        }

        private void SetView()
        {
            selectPanel.Visible = loadingPanel.Visible = !Uploaded;
            uploadedPanel.Visible = Uploaded;
        }

        private FileSelect GetFileSelect(Panel panel)
        {
            var fileSelects = panel.Controls.OfType<FileSelect>();
            if (fileSelects.Count() != 1)
                throw new InvalidOperationException(
                    "The SelectTemplate must contain exactly one FileSelect control.");
            return fileSelects.First();
        }

        public string UploadManagerId
        {
            get
            {
                var id = Page.Request["sf_uploadManagerId"];
                if(string.IsNullOrEmpty(id))
                    throw new Exception("The url parameter sf_uploadManagerId is missing or it's value is empty.");
                return id;
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
