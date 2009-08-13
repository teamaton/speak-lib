using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.Utilities
{
    public class FileStore : RepositoryDb<StoredFile, StoredFileList>
    {
        private readonly string _pathAbsolute;
        private readonly string _pathRelative;

        public FileStore(ISession session, string pathAbsolute, string pathRelative) : base(session)
        {
            _pathAbsolute = pathAbsolute;
            _pathRelative = pathRelative;
        }

        public StoredFile Store(string sourcePath, IPersistable entity)
        {
            return Store(sourcePath, entity, sourcePath);
        }

        public StoredFile Store(string sourcePath, IPersistable entity, string fileName)
        {
            var storedFile = new StoredFile
            {
                OriginalFileName = Path.GetFileName(fileName),
                EntityType = "FIXME!",
                EntityId = entity.Id
            };

            Create(storedFile);

            File.Copy(sourcePath, GetPathAbsolute(storedFile));

            return storedFile;
        }

        public string GetPathAbsolute(StoredFile storedFile)
        {
            return Path.Combine(_pathAbsolute, GetFileName(storedFile));
        }

        public string GetFileName(StoredFile storedFile)
        {
            return string.Format("File{0}.dat", storedFile.Id);
        }
       
    }
}
