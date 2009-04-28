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
        private readonly List<int> _selectedIds = new List<int>();

        private int _currentId
        {
            get
            {
                var value = ViewState["currentId"];
                return value is int ? (int) value : 0;
            }
            set
            {
                ViewState["currentId"] = value;
            }
        }

        private ImageSort _imageSort
        {
            get
            {
                var value = ViewState["imageSort"];
                return value is ImageSort ? (ImageSort) value : ImageSort.None;
            }
            set
            {
                ViewState["imageSort"] = value;
            }
        }

        private bool _reverseSort
        {
            get
            {
                var value = ViewState["reverseSort"];
                return value is bool ? (bool) value : false;
            }
            set
            {
                ViewState["reverseSort"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpload.Click += btnUpload_Click;
            btnSaveUpload.Click += btnSaveUpload_Click;

            rptFiles.ItemCommand += rptFiles_ItemCommand;
            btnDelete.Click += btnDelete_Click;
            btnSelectAll.Click += ((sender1, e1) => SetAllChecked(true));

            btnOrderByFileName.Click += btnOrderByFileName_Click;
            btnOrderBySize.Click += btnOrderBySize_Click;
            btnOrderByType.Click += btnOrderByType_Click;
            btnOrderByDate.Click += btnOrderByDate_Click;
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

        private List<ImageInfo> GetImages()
        {
            var images = _imageStore.GetGroup(_groupKey, _imageSort);
            if (_reverseSort) images.Reverse();
            return images;
        }

        private void FindSelectedItems()
        {
            _selectedIds.Clear();
            var images = GetImages();
            foreach(var item in rptFiles.Items.OfType<RepeaterItem>())
            {
                var itemHelper = new ItemTemplateHelper(item);
                if (itemHelper.Find<CheckBox>("cbSelectItem").Checked)
                    _selectedIds.Add(images[item.ItemIndex].Id);
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

        private void SetImageSort(ImageSort sort)
        {
            if (_imageSort == sort) _reverseSort = !_reverseSort;
            else
            {
                _imageSort = sort;
                _reverseSort = false;
            }
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
            btnSize.Text = string.Format("{0} KB", Math.Ceiling(image.FileSize/1024d));
            btnType.Text = image.FileExtension;
            btnDate.Text = image.CreationTime.ToString("dd.MM.yyyy");

            foreach (var button in new[] { btnName, btnSize, btnType, btnDate })
            {
                button.CommandName = "selectItem";
                button.CommandArgument = image.Id.ToString();
            }

            if(image.Id == _currentId)
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
                    int currentId;
                    int.TryParse(e.CommandArgument.ToString(), out currentId);
                    _currentId = currentId;
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
            var extension = Path.GetExtension(file.Name);
            var image = _imageStore.StoreToGroup(_groupKey, file.TempFilePathAbsolute,
                                                 Path.GetFileNameWithoutExtension(file.Name),
                                                 extension == ".jpg" || extension == ".jpeg");
            _currentId = image.Id;
            mvPreview.SetActiveView(vwImagePreview);
            Populate();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (var id in _selectedIds)
                _imageStore.Delete(_groupKey, id);
            Populate();
        }

        void btnOrderByFileName_Click(object sender, EventArgs e)
        {
            SetImageSort(ImageSort.Name);
            Populate();
        }

        void btnOrderBySize_Click(object sender, EventArgs e)
        {
            SetImageSort(ImageSort.Size);
            Populate();
        }

        void btnOrderByType_Click(object sender, EventArgs e)
        {
            SetImageSort(ImageSort.Type);
            Populate();
        }

        void btnOrderByDate_Click(object sender, EventArgs e)
        {
            SetImageSort(ImageSort.Date);
            Populate();
        }
    }
}