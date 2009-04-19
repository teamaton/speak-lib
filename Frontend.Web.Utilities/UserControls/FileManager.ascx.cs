using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Web;
using System.Web.UI.HtmlControls;

namespace SpeakFriend.Web.Utilities.UserControls
{
    public partial class FileManager : System.Web.UI.UserControl
    {
        private const int previewMaxWidth = 100;
        private const int previewMaxHeight = 100;

        private ImageStore _imageStore;
        private string _groupKey;
        private int selectedId;

        protected void Page_Load(object sender, EventArgs e)
        {
            rptFiles.ItemCommand += rptFiles_ItemCommand;
        }

        void rptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "selectItem":
                    int.TryParse(e.CommandArgument.ToString(), out selectedId);
                    Populate();
                    break;
            }
        }

        public void Register(ImageStore imageStore, string groupKey)
        {
            _imageStore = imageStore;
            _groupKey = groupKey;

            Populate();
        }

        private void Populate()
        {
            rptFiles.DataSource = _imageStore.GetGroup(_groupKey);
            rptFiles.ItemDataBound += rptFiles_ItemDataBound;
            rptFiles.DataBind();
        }

        void rptFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!ItemTemplateHelper.IsContentItem(e.Item))
                return;

            var image = (ImageInfo)e.Item.DataItem;
            var itemHelper = new ItemTemplateHelper(e.Item);

            var btnName = itemHelper.Find<LinkButton>("btnName");
            var btnSize = itemHelper.Find<LinkButton>("btnSize");
            var btnType = itemHelper.Find<LinkButton>("btnType");
            var btnDate = itemHelper.Find<LinkButton>("btnDate");

            btnName.Text = image.Name;
            btnSize.Text = "unknown";
            btnType.Text = Path.GetExtension(image.AbsolutePath);
            btnDate.Text = File.GetCreationTime(image.AbsolutePath).ToString();

            foreach (var button in new[] { btnName, btnSize, btnType, btnDate })
            {
                button.CommandName = "selectItem";
                button.CommandArgument = image.Id.ToString();
            }

            if(image.Id == selectedId)
            {
                itemHelper.Find<Panel>("pnlItem").AddCssClass("current");
                PopulateSelectedItemPanel(image);
            }
        }

        private void PopulateSelectedItemPanel(ImageInfo image)
        {
            pnlSelectedItemDetails.Visible = true;
            ltName.Text = image.Name;
            imgPreview.ImageUrl =
                _imageStore.GetThumb(_groupKey, image.Id, new Size(previewMaxWidth, previewMaxHeight)).RelativePath;
            imgOriginal.ImageUrl = image.RelativePath;
        }
    }
}