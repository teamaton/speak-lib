using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ImageStore
    {
        private readonly string _pathAbsolute;
        private readonly string _pathRelative;

        public ImageStore(string pathAbsolute, string pathRelative)
        {
            _pathAbsolute = pathAbsolute;
            _pathRelative = pathRelative;
        }

        public void Store(string imageKey, string sourcePath)
        {

            var image = Image.FromFile(sourcePath);
            image.Save(GetPathAbsolute(imageKey), ImageFormat.Png);
        }

        public void StoreToGroup(string groupKey, string sourcePath, string name)
        {
            var groupDirectory = GetGroupDirectoryAbsolute(groupKey);
            Directory.CreateDirectory(groupDirectory);

            var images = GetGroup(groupKey);
            var id = images.Count == 0 ? 0 : images.Max(img => img.Id) + 1;
            var path = GetPathAbsolute(groupKey, string.Format("{0}-{1}.png", id, name));

            var image = Image.FromFile(sourcePath);
            image.Save(path, ImageFormat.Png);
        }
  
        public ImageInfo Get(string imageKey)
        {
            //ToDo: check if exists and if not, return default placeholder
            return new ImageInfo
                       {
                           AbsolutePath = GetPathAbsolute(imageKey),
                           RelativePath = GetPathRelative(imageKey),
                           Name = imageKey,
                           Id = -1
                       };
        }


        public ImageInfo Get(string groupKey, int id)
        {
            return GetGroup(groupKey).Find(image => image.Id == id);
        }

        public List<ImageInfo> GetGroup(string groupKey)
        {
            if(!Directory.Exists(GetGroupDirectoryAbsolute(groupKey))) 
                return new List<ImageInfo>();

            return
                Directory.GetFiles(GetGroupDirectoryAbsolute(groupKey)).ToList().ConvertAll(
                    path =>
                        {
                            var filename = Path.GetFileNameWithoutExtension(path);
                            var extension = Path.GetExtension(path);
                            return new ImageInfo()
                                       {
                                           Id = Convert.ToInt32(filename.Split('-').First()),
                                           AbsolutePath = path,
                                           RelativePath = GetPathRelative(groupKey, filename + extension),
                                           Name = filename.Substring(filename.IndexOf('-') + 1)
                                       };
                        });
            
        }

        private string GetPathRelative(string groupKey, string filename)
        {
            return Path.Combine(Path.Combine(_pathRelative, groupKey), filename);
        }

        private string GetPathAbsolute(string groupKey, string filename)
        {
            return Path.Combine(Path.Combine(_pathAbsolute, groupKey), filename);
        }

        private string GetGroupDirectoryAbsolute(string groupKey)
        {
            return Path.Combine(_pathAbsolute, groupKey);
        }

        private string GetPathAbsolute(string imageKey)
        {
            return Path.Combine(_pathAbsolute, string.Format("{0}.png", imageKey));
        }

        private string GetPathRelative(string imageKey)
        {
            return Path.Combine(_pathRelative, string.Format("{0}.png", imageKey));
        }

    }
}
