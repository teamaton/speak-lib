using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using SpeakFriend.Utilities;

namespace SpeakFriend.FileUpload
{
    public class UploadManager : IDisposable
    {
        private readonly List<UploadedFile> files = new List<UploadedFile>();
        private List<UploadedFile> Files
        {
            get
            {
                if (disposed) throw new ObjectDisposedException("UploadManager");
                return files;
            }
        }

        public ReadOnlyCollection<UploadedFile> UploadedFiles
        {
            get { return Files.AsReadOnly(); }
        }

        public UploadedFile HandleFile(HttpPostedFile postedFile)
        {
            var uploadedFile = new UploadedFile {Name = postedFile.FileName};
            postedFile.SaveAs(uploadedFile.TempFilePathAbsolute);
            Files.Add(uploadedFile);
            return uploadedFile;
        }

        private static void DeleteFile(UploadedFile file)
        {
            if (File.Exists(file.TempFilePathAbsolute))
                File.Delete(file.TempFilePathAbsolute);
        }        
        
        public void RemoveFile(UploadedFile file)
        {
            if(!Files.Contains(file))
                throw new ArgumentException("The file to be removed is not managed by this UploadManager");

            DeleteFile(file);
            Files.Remove(file);
        }

        public void RemoveFile(Guid tempKey)
        {
            RemoveFile(Files.Find(file => file.TempKey == tempKey));
        }

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (disposed) return;

            foreach (var file in Files)
                DeleteFile(file);

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UploadManager()
        {
            Dispose(false);
        }
    }
}
