using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
    public class BaseTest : AssertionHelper
    {
        protected static readonly UsefulnessService _usefulnessService = ServiceLocator.UsefulnessService();
		protected static readonly UsefulEntityService _usefulEntityService = ServiceLocator.UsefulEntityService();
		protected static readonly NHibernateHelperSF _nHibernateHelper = ServiceLocator.NHibernateHelper();
    }
}
