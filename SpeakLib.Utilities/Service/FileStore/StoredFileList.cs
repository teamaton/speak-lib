using System;
using System.Collections.Generic;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class StoredFileList : PersistableList<StoredFile>
    {
        public StoredFileList()
        {
        }        
        
        public StoredFileList(IEnumerable<StoredFile> files): base(files)
        {
        }
    }
}