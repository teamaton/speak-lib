using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SpeakFriend.TrueOrFalse;
using Web.TrueOrFalse.Code;

namespace Web.TrueOrFalse.Code
{
    public class BasePage : Page
    {
        //protected static UserSession _userSession { get { return ServiceLocator.UserSession; } }

        protected static QuestionService _questionService { get { return ServiceLocator.QuestionService; } }
    }
}
