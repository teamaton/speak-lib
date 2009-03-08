/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web
{
    public class ViewStateController : PageStatePersister
    {
        private const string _viewStateKey = "__STATE";
        private const ViewStateStoreType _viewStateStoreType = ViewStateStoreType.Cache;
        private readonly IViewStateStore _store;

        public ViewStateController(Page page) : base(page)
        {
            switch (_viewStateStoreType)
            {
                case ViewStateStoreType.Cache:
                    _store = new ViewStoreCache();
                    break;
                case ViewStateStoreType.Session:
                    _store = new ViewStateSession();
                    break;
                default:
                    throw new Exception("unknown store");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {   
            string viewStateKey = Page.Request.Form[_viewStateKey];
            Pair statePair = _store.Load(viewStateKey);
            
            if (statePair == null)
                return;

            ViewState = statePair.First;
            ControlState = statePair.Second;
        }

        public override void Save()
        {
            string viewStateKey = "STATE-" + Guid.NewGuid().ToString().ToUpper();
            _store.Save(viewStateKey, new Pair(ViewState, ControlState));

            Page.ClientScript.RegisterHiddenField(_viewStateKey, viewStateKey);
        }
    }
}
