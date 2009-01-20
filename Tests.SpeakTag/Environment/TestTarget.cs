using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakTag.Tests.Environment
{
    public class TestTarget : ITaggable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
