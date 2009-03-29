using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.FileUpload
{
    [ToolboxData("<{0}:UploadedFileDelete runat=server></{0}:UploadedFileDelete>")]
    public class UploadedFileDelete : UploadedFileControl
    {

        private LinkButton btnDelete;

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        DefaultValue(typeof(ITemplate), ""),
        Description("Control template"),
        TemplateContainer(typeof(FileUploadContent))
        ]
        public virtual ITemplate ButtonContentTemplate { get; set; }
        sealed class DefaultButtonContentTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control owner)
            {
                var lblDelete = new Label(){Text="[Delete]"};
                owner.Controls.Add(lblDelete);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EnsureChildControls();
            if (btnDelete != null)
                btnDelete.Click += btnDelete_Click;

        }
        
        void btnDelete_Click(object sender, EventArgs e)
        {
            UploadManager.RemoveFile(File.TempKey);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (btnDelete == null)
                btnDelete = new LinkButton
                                {
                                    ID = "btnDelete"
                                };

            if (ButtonContentTemplate == null)
                ButtonContentTemplate = new DefaultButtonContentTemplate();

            ButtonContentTemplate.InstantiateIn(btnDelete);

            Controls.Add(btnDelete);
        }
    }
}
