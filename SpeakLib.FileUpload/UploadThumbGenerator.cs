using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.FileUpload
{
	public class UploadThumbGenerator : IDisposable
	{
		private readonly UploadManager _uploadManager;

		private readonly Dictionary<Guid, Dictionary<int, UploadThumb>> _thumbs =
			new Dictionary<Guid, Dictionary<int, UploadThumb>>();

		public UploadThumbGenerator(UploadManager uploadManager)
		{
			_uploadManager = uploadManager;
			_uploadManager.Disposed += UploadManager_Disposed;
		}

		public UploadThumb GetThumb(UploadedFile file, int width)
		{
			if (disposed) throw new ObjectDisposedException("UploadThumbGenerator");

			if (!_thumbs.ContainsKey(file.TempKey))
				_thumbs.Add(file.TempKey, new Dictionary<int, UploadThumb>());

			var fileThumbs = _thumbs[file.TempKey];

			if (fileThumbs.ContainsKey(width))
				return (fileThumbs[width]);

			var thumb = new UploadThumb();
			try
			{
				using (var image = Image.FromFile(file.TempFilePathAbsolute))
				using (var resized = image.Width <= width
				                     	? image
				                     	: ImageUtils.ResizeImage(image, width, false))
					resized.Save(thumb.ThumbPathAbsolute, ImageFormat.Png);
				fileThumbs.Add(width, thumb);
				return thumb;
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (Exception)
			{
				DeleteThumb(thumb);
				return null;
			}
		}

		public void DeleteThumbs(UploadedFile file)
		{
			if (!_thumbs.ContainsKey(file.TempKey))
				return;

			var fileThumbs = _thumbs[file.TempKey];
			foreach (var thumb in fileThumbs.Values)
				DeleteThumb(thumb);

			_thumbs.Remove(file.TempKey);
		}

		private void DeleteThumb(UploadThumb thumb)
		{
			if (File.Exists(thumb.ThumbPathAbsolute))
				File.Delete(thumb.ThumbPathAbsolute);
		}

		private void UploadManager_Disposed(object sender, EventArgs e)
		{
			Dispose();
		}

		private bool disposed;

		private void Dispose(bool disposing)
		{
			if (disposed) return;

			foreach (var fileThumbs in _thumbs.Values)
				foreach (var thumb in fileThumbs.Values)
					DeleteThumb(thumb);

			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~UploadThumbGenerator()
		{
			Dispose(false);
		}
	}
}