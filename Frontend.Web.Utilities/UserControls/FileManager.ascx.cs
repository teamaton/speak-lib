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
        private int currentId;
        private readonly List<int> selectedIds = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpload.Click += btnUpload_Click;
            btnSaveUpload.Click += btnSaveUpload_Click;

            rptFiles.ItemCommand += rptFiles_ItemCommand;
            btnDelete.Click += btnDelete_Click;
            btnSelectAll.Click += ((sender1, e1) => SetAllChecked(true));
        }

        public void Register(ImageStore imageStore, string groupKey)
        {
            _imageStore = imageStore;
            _groupKey = groupKey;

            FindSelectedItems();
            Populate();
        }

        private void Populate()
        {
            rptFiles.DataSource = GetImages();
            rptFiles.ItemDataBound += rptFiles_ItemDataBound;
            rptFiles.DataBind();
        }

        private void PopulateSelectedItemPanel(ImageInfo image)
        {
            mvPreview.SetActiveView(vwImagePreview);
            ltName.Text = image.Name;
            imgPreview.ImageUrl =
                _imageStore.GetThumb(_groupKey, image.Id, new Size(previewMaxWidth, previewMaxHeight)).RelativePath;
            imgOriginal.ImageUrl = image.RelativePath;
        }

        private void FindSelectedItems()
        {
            selectedIds.Clear();
            var images = GetImages();
            foreach(var item in rptFiles.Items.OfType<RepeaterItem>())
            {
                var itemHelper = new ItemTemplateHelper(item);
                if (itemHelper.Find<CheckBox>("cbSelectItem").Checked)
                    selectedIds.Add(images[item.ItemIndex].Id);
            }
        }

        private void SetAllChecked(bool value)
        {
            foreach (var item in rptFiles.Items.OfType<RepeaterItem>())
            {
                var itemHelper = new ItemTemplateHelper(item);
                itemHelper.Find<CheckBox>("cbSelectItem").Checked = value;
            }
        }

        private List<ImageInfo> GetImages()
        {
            return _imageStore.GetGroup(_groupKey);
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
            btnSize.Text = string.Format("{0} KB", Math.Ceiling(new FileInfo(image.AbsolutePath).Length/1024d));
            btnType.Text = Path.GetExtension(image.AbsolutePath);
            btnDate.Text = File.GetCreationTime(image.AbsolutePath).ToString("dd.MM.yyyy");

            foreach (var button in new[] { btnName, btnSize, btnType, btnDate })
            {
                button.CommandName = "selectItem";
                button.CommandArgument = image.Id.ToString();
            }

            if(image.Id == currentId)
            {
                itemHelper.Find<Panel>("pnlItem").AddCssClass("current");
                PopulateSelectedItemPanel(image);
            }
        }

        void rptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "selectItem":
                    int.TryParse(e.CommandArgument.ToString(), out currentId);
                    Populate();
                    break;
            }
        }

        void btnUpload_Click(object sender, EventArgs e)
        {
            mvPreview.SetActiveView(vwUpload);
        }

        void btnSaveUpload_Click(object sender, EventArgs e)
        {
            var file = fufUpload.UploadedFiles.Last();
            var image = _imageStore.StoreToGroup(_groupKey, file.TempFilePathAbsolute,
                                                 Path.GetFileNameWithoutExtension(file.Name));
            currentId = image.Id;
            mvPreview.SetActiveView(vwImagePreview);
            Populate();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (var id in selectedIds)
                _imageStore.Delete(_groupKey, id);
            Populate();
        }


    }
}