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
        private const string _viewStateKey = "__CVSTATE";
        private static ViewStateStoreType _viewStateStoreType { get; set; }

        public ViewStateController(Page page) : base(page)
        {
            _viewStateStoreType = ViewStateStoreType.Cache;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            string viewStateKey = Page.Request.Form[_viewStateKey];
            
            IViewStateStore store = GetViewSateStore();

            byte[]  buffer = new ASCIIEncoding().GetBytes(store.Load(viewStateKey));

            var memoryStream = new MemoryStream(100);
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(memoryStream, Encoding.ASCII);
            IStateFormatter formatter = StateFormatter;
            Pair statePair = (Pair)formatter.Deserialize(streamReader.ReadToEnd());
            ViewState = statePair.First;
            ControlState = statePair.Second;
            streamReader.Close();
            memoryStream.Close();
        }

        private IViewStateStore GetViewSateStore()
        {
            IViewStateStore store;
            switch (_viewStateStoreType)
            {
                case ViewStateStoreType.Cache:
                    store = new ViewStoreCache();
                    break;
                case ViewStateStoreType.Session:
                    store = new ViewStateSession();
                    break;
                default:
                    throw new Exception("unknown store");
            }
            return store;
        }

        public override void Save()
        {
            var memoryStream = new MemoryStream(100);
            var streamWriter = new StreamWriter(memoryStream, Encoding.ASCII);
            var formatter = StateFormatter;
            var statePair = new Pair(ViewState, ControlState);
            string serializedData = formatter.Serialize(statePair);
            streamWriter.Write(serializedData);
            streamWriter.Close();
            memoryStream.Close();
            
            
            string viewStateKey = "CVSTATE-" + Guid.NewGuid().ToString().ToUpper();

            IViewStateStore store = GetViewSateStore();
            store.Save(viewStateKey, serializedData);

            Page.ClientScript.RegisterHiddenField(_viewStateKey, viewStateKey);
        }
    }
}
