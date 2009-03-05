/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace SpeakFriend.Utilities.Web
{
    public class ViewStoreCache : IViewStateStore
    {
        public void Save(string id, object data)
        {
            HttpContext.Current.Cache.Add(id, data, null, DateTime.Now.AddMinutes(20),
                System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public string Load(string id)
        {
            string returnValue = string.Empty;
            if (HttpContext.Current.Cache[id] != null) 
            {
                returnValue = HttpContext.Current.Cache[id].ToString();
            }
            return returnValue;
        }

        public void Delete(string id)
        {
            if (HttpContext.Current.Cache[id] != null)
                HttpContext.Current.Cache.Remove(id);
        }
    }
}
