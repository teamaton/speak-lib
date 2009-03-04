using System;
using System.Collections.Generic;
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
        private readonly List<UploadedFile> uploadedFiles = new List<UploadedFile>();

        public UploadedFile HandleFile(HttpPostedFile postedFile)
        {
            var uploadedFile = new UploadedFile {Name = postedFile.FileName};
            postedFile.SaveAs(uploadedFile.TempFileName);
            uploadedFiles.Add(uploadedFile);
            return uploadedFile;
        }

        private static void DeleteFile(UploadedFile file)
        {
            if (File.Exists(file.TempFileName))
                File.Delete(file.TempFileName);
            file.Delete();
        }        
        
        public void RemoveFile(UploadedFile file)
        {
            DeleteFile(file);
            uploadedFiles.Remove(file);
        }

        public void RemoveFile(Guid tempKey)
        {
            RemoveFile(uploadedFiles.Find(file => file.TempKey == tempKey));
        }
        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (disposed) return;

            foreach (var file in uploadedFiles)
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
