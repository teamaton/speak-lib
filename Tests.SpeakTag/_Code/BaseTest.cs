using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakTag.Tests.Environment;

namespace SpeakTag.Tests._Code
{
    public class BaseTest
    {
        protected static readonly TestTargetService _targetService = ServiceLocator.TestTargetService();
        protected static readonly TagPrototypeService _prototypeService = ServiceLocator.TagPrototypeService();
        protected static readonly TagService _tagService = ServiceLocator.TagService();
    }
}
