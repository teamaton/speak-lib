/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web
{
    public class ViewStateSession : IViewStateStore
    {
        public void Save(string id, Pair data)
        {
            HttpContext.Current.Session.Add(id, data);
        }

        public Pair Load(string id)
        {
            if (HttpContext.Current.Session[id] != null) 
                return (Pair)HttpContext.Current.Session[id];

            return null;
        }

        public void Delete(string id)
        {
            if (HttpContext.Current.Session[id] != null)
                HttpContext.Current.Session.Remove(id);
        }
    }
}
