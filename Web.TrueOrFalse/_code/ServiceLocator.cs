using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeakFriend.TrueOrFalse;

namespace Web.TrueOrFalse.Code
{
    public class ServiceLocator
    {
        public static QuestionService QuestionService { get { return Resolve<QuestionService>(); } }

        private static T Resolve<T>()
        {
            return Global.ContainerProvider.RequestContainer.Resolve<T>();
        }

    }


}
