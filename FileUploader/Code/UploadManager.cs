using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SpeakFriend.Utils;

namespace SpeakFriend.FileUploader
{
    public class UploadManager : IDisposable
    {

        public string TempPathAbsolute
        {
            get
            {
                return UploadSettings.ImagesDirAbsolute + "\\temp";
            }
        }

        public string TempPathRelative
        {
            get
            {
                return UploadSettings.ImagesDirRelative + "/temp";
            }
        }

        public Dictionary<int, string> ImagePaths { get; private set; }
        private readonly List<string> _thumbPaths = new List<string>();

        public void ManageUploadedImage(Stream data, int index, out string imageName, out string thumbName)
        {
            var buffer = new byte[data.Length];
            data.Read(buffer, 0, (int)data.Length);
            data.Close();

            Image img;

            try{
                img = Image.FromStream(new MemoryStream(buffer));
            }catch (ArgumentException){
                throw new ArgumentException("invalid image source");
            }

            var id = img.GetHashCode();

            var thumb = ImageService.ResizeImage(img, 40, true);
            img.Dispose();

            imageName = String.Format("{0}.tmp", id);
            thumbName = String.Format("{0}_thumb.jpg", id);

            var imagePath = Path.Combine(TempPathAbsolute, imageName);
            var thumbPath = Path.Combine(TempPathAbsolute, thumbName);

            if (!Directory.Exists(TempPathAbsolute))
                Directory.CreateDirectory(TempPathAbsolute);

            var file = new FileStream(imagePath, FileMode.Create);
            
            file.Write(buffer,0,buffer.Length);
            file.Close();
            
            thumb.Save(thumbPath,ImageFormat.Jpeg);
            thumb.Dispose();

            ImagePaths.Add(index, imagePath);
            _thumbPaths.Add(thumbPath);
        }

        public void DeleteImage(int index)
        {
            if (!ImagePaths.ContainsKey(index))
                return;

            File.Delete(ImagePaths[index]);
            ImagePaths.Remove(index);

            //Note: thumbs are deleted on dispose.
        }

        public void Dispose()
        {
            foreach (var list in new IEnumerable<string>[] { ImagePaths.Values, _thumbPaths })
                foreach (var file in list)
                    File.Delete(file);
        }

        public UploadManager()
        {
            ImagePaths = new Dictionary<int, string>();
        }
    }
}
