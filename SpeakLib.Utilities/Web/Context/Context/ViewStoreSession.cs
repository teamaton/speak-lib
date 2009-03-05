/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class ViewStateSession : IViewStateStore
    {
        public void Save(string id, object data)
        {
            HttpContext.Current.Session.Add(id, data);
        }

        public string Load(string id)
        {
            string returnValue = string.Empty;
            
            if (HttpContext.Current.Session[id] != null) 
                return HttpContext.Current.Session[id].ToString();

            return returnValue;
        }

        public void Delete(string id)
        {
            if (HttpContext.Current.Session[id] != null)
                HttpContext.Current.Session.Remove(id);
        }
    }
}
