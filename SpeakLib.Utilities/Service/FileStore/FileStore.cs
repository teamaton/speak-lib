using System;
using System.Collections.Generic;
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

        public StoredFile Store(string sourcePath)
        {
            throw new NotImplementedException();
        }
    }
}
