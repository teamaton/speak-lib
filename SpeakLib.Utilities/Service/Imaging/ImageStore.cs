using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.Utilities
{
    public class ImageStore
    {
        private readonly AppData _appData = new AppData();
        private const string _appDataKey = "imageStore_";

        private readonly string _pathAbsolute;
        private readonly string _pathRelative;
        private const string _pathThumbs = "thumbs";

        public ImageStore(string pathAbsolute, string pathRelative)
        {
            _pathAbsolute = pathAbsolute;
            _pathRelative = pathRelative;
        }

        public void Store(string imageKey, string sourcePath, bool useJpeg)
        {
            if (useJpeg)
            {
                File.Delete(GetPathAbsolute(imageKey, false));

                //copy original file to avoid quality loss
                var path = GetPathAbsolute(imageKey, true);
                File.Copy(sourcePath, path, true);
                CalculateHashCode(path, imageKey);
            }
            else
            {
                using (var image = Image.FromFile(sourcePath))
                    Store(imageKey, image, false);
            }
        }

        public void Store(string imageKey, Image image, bool useJpeg)
        {
            File.Delete(GetPathAbsolute(imageKey, false));
            File.Delete(GetPathAbsolute(imageKey, true));

            var path = GetPathAbsolute(imageKey, useJpeg);
            image.Save(path, useJpeg ? ImageFormat.Jpeg : ImageFormat.Png);
            CalculateHashCode(path, imageKey);
        }

        public ImageInfo StoreToGroup(string groupKey, string sourcePath, string name, bool useJpeg)
        {
            var groupDirectory = GetGroupDirectoryAbsolute(groupKey);
            Directory.CreateDirectory(groupDirectory);

            var images = GetGroup(groupKey);
            var id = images.Count == 0 ? 1 : images.Max(img => img.Id) + 1;
            //var path = GetPathAbsolute(groupKey, id, name, useJpeg);

            var path = GetPathAbsolute(groupKey, id, name, useJpeg);

            if (useJpeg)
            {
                File.Delete(GetPathAbsolute(groupKey, id, name, false));

                //copy original file to avoid quality loss
                File.Copy(sourcePath, path, true);
            }
            else
            {
                using (var image = Image.FromFile(sourcePath))
                    image.Save(path, ImageFormat.Png);
            }

            return Get(groupKey, id);
        }
  
        public string Get(string imageKey)
        {
            var useJpeg = File.Exists(GetPathAbsolute(imageKey, true));
        	return GetPathAbsolute(imageKey, useJpeg);
        }

        public string GetThumb(string imageKey, Size imageSize)
        {
			var useJpeg = File.Exists(GetPathAbsolute(imageKey, true));

			if (!File.Exists(GetPathAbsolute(imageKey, useJpeg)))
				return null;

        	return EnsureThumb(imageKey, useJpeg, imageSize);
        }

    	public void EnforceUpdate(string imageKey)
    	{
    		var key = _appDataKey + imageKey;
    		_appData[key] = null;
    	}

    	public ImageInfo Get(string groupKey, int id)
        {
            return GetGroup(groupKey).Find(image => image.Id == id);
        }

        public ImageInfo GetThumb(string groupKey, int id, Size maxSize)
        {
            var image = Get(groupKey, id);

            if (image == null) return null;

            EnsureHashCode(image);

            var thumb = new ImageInfo
                            {
                                AbsolutePath = GetThumbPathAbsolute(image, maxSize),
                                RelativePath = GetThumbPathRelative(image, maxSize),
                                Name = image.Name,
                                GroupKey = image.GroupKey,
                                Id = image.Id,
                                HashCode = image.HashCode
                            };

            EnsureThumb(image, thumb, maxSize);

            return thumb;
        }

        public List<ImageInfo> GetGroup(string groupKey)
        {
            return GetGroup(groupKey, ImageSort.None);
        }

        public List<ImageInfo> GetGroup(string groupKey, ImageSort imageSort)
        {
            if(!Directory.Exists(GetGroupDirectoryAbsolute(groupKey))) 
                return new List<ImageInfo>();

            var result = Directory.GetFiles(GetGroupDirectoryAbsolute(groupKey)).ToList().ConvertAll(
                path =>
                    {
                        var filename = Path.GetFileNameWithoutExtension(path);
                        var extension = Path.GetExtension(path);
                        return new ImageInfo
                                   {
                                       GroupKey = groupKey,
                                       Id = Convert.ToInt32(filename.Split('-').First()),
                                       AbsolutePath = path,
                                       RelativePath = GetPathRelative(groupKey, filename + extension),
                                       Name = filename.Substring(filename.IndexOf('-') + 1),
                                       UseJpeg = extension == ".jpg"
                                   };
                    });

            if(imageSort == ImageSort.None) return result;

            Comparison<ImageInfo> comparison;

            switch(imageSort)
            {
                case ImageSort.Name:
                    comparison = (i1, i2) => i1.Name.CompareTo(i2.Name);
                    break;
                case ImageSort.Size:
                    comparison = (i1, i2) => i1.FileSize.CompareTo(i2.FileSize);
                    break;
                case ImageSort.Type:
                    comparison = (i1, i2) => i1.FileExtension.CompareTo(i2.FileExtension);
                    break;
                case ImageSort.Date:
                    comparison = (i1, i2) => i1.CreationTime.CompareTo(i2.CreationTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("imageSort");
            }

            result.Sort(comparison);

            return result;
        }

        private void EnsureThumb(ImageInfo original, ImageInfo thumb, Size maxSize)
        {
            if (thumb.HashCode == 0) return;

            if (File.Exists(thumb.AbsolutePath)) return;

            var sourcePath = original.AbsolutePath;
            if (!File.Exists(sourcePath)) return;

            using (var image = Image.FromFile(sourcePath))
            using (var resized = image.Width <= maxSize.Width && image.Height <= maxSize.Height
                                     ? image
                                     : maxSize.Width != 0
                                       && (maxSize.Height == 0
                                           || (double) image.Width/image.Height
                                              >= (double) maxSize.Width/maxSize.Height)
                                           ? ImageUtils.ResizeImage(image, maxSize.Width, false)
                                           : ImageUtils.ResizeImage(image, maxSize.Height, true))
                resized.Save(thumb.AbsolutePath, thumb.UseJpeg ? ImageFormat.Jpeg : ImageFormat.Png);
        }

    	private string EnsureThumb(string imageKey, bool useJpeg, Size imageSize)
    	{
    		var thumbPath = GetThumbPathAbsolute(imageKey, useJpeg, imageSize);
			if (File.Exists(thumbPath)) 
				return thumbPath;

			using (var image = Image.FromFile(GetPathAbsolute(imageKey, useJpeg)))
			using (var resized = image.Width <= imageSize.Width && image.Height <= imageSize.Height
									 ? image
									 : imageSize.Width != 0
									   && (imageSize.Height == 0
										   || (double)image.Width / image.Height
											  >= (double)imageSize.Width / imageSize.Height)
										   ? ImageUtils.ResizeImage(image, imageSize.Width, false)
										   : ImageUtils.ResizeImage(image, imageSize.Height, true))
				resized.Save(thumbPath, useJpeg ? ImageFormat.Jpeg : ImageFormat.Png);

    		return thumbPath;
		}

    	private string GetThumbPathRelative(ImageInfo image, Size maxSize)
        {
            return Path.Combine(
                Path.Combine(_pathRelative, _pathThumbs),
                string.Format("{0}_{1}x{2}px.{3}", image.HashCode, maxSize.Width, maxSize.Height,
                              image.UseJpeg ? "jpg" : "png"));
        }

        private string GetThumbPathAbsolute(ImageInfo image, Size maxSize)
        {
            return Path.Combine(
                Path.Combine(_pathAbsolute, _pathThumbs),
                string.Format("{0}_{1}x{2}px.{3}", image.HashCode, maxSize.Width, maxSize.Height,
                              image.UseJpeg ? "jpg" : "png"));
        }

    	private string GetThumbPathAbsolute(string key, bool jpeg, Size imageSize)
    	{
			return Path.Combine(
				Path.Combine(_pathAbsolute, _pathThumbs),
				string.Format("{0}_{1}x{2}px.{3}", key, imageSize.Width, imageSize.Height, jpeg ? "jpg" : "png"));
		}

    	public string GetThumbDirectoryRelative()
    	{
    		return Path.Combine(_pathRelative, _pathThumbs);
    	}

    	private void EnsureHashCode(ImageInfo info)
        {
            var imageKey = info.GroupKey != null ? info.GroupKey + info.Id : info.Name;
            var key = _appDataKey + imageKey;
            
            if (_appData[key] == null)
                CalculateHashCode(info.AbsolutePath, imageKey);

            info.HashCode = (int) (_appData[key] ?? 0);
        }

        private void CalculateHashCode(string path, string imageKey)
        {
            var key = _appDataKey + imageKey;

            if (!File.Exists(path))
            {
                _appData[key] = null;
                return;
            }

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, stream.Length < int.MaxValue ? (int) stream.Length : int.MaxValue);

                _appData[key] = data.GetHashCode();
            }
        }


        private string GetPathRelative(string groupKey, string filename)
        {
            return Path.Combine(Path.Combine(_pathRelative, groupKey), filename);
        }

        private string GetPathAbsolute(string groupKey, int id, string name, bool useJpeg)
        {
            return Path.Combine(Path.Combine(_pathAbsolute, groupKey), GetFilename(id, name, useJpeg));
        }

        private string GetFilename(int id, string name, bool useJpeg)
        {
            return string.Format("{0}-{1}.{2}", id, name, useJpeg ? "jpg" : "png");
        }

        private string GetGroupDirectoryAbsolute(string groupKey)
        {
            return Path.Combine(_pathAbsolute, groupKey);
        }

        private string GetPathAbsolute(string imageKey, bool useJpeg)
        {
            return Path.Combine(_pathAbsolute, string.Format("{0}.{1}", imageKey, useJpeg ? "jpg" : "png"));
        }

    	public void Delete(string groupKey, int id)
        {
            var file = GetGroup(groupKey).Find(image => image.Id == id);
            if (file != null) File.Delete(file.AbsolutePath);
        }
    }
}
