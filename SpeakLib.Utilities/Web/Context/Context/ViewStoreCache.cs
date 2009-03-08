/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web
{
    public class ViewStoreCache : IViewStateStore
    {
        public void Save(string id, Pair data)
        {
            HttpContext.Current.Cache.Add(id, data, null, DateTime.Now.AddMinutes(20),
                System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public Pair Load(string id)
        {
            if (HttpContext.Current.Cache[id] != null) 
              return (Pair)HttpContext.Current.Cache[id];
            
            return null;
        }

        public void Delete(string id)
        {
            if (HttpContext.Current.Cache[id] != null)
                HttpContext.Current.Cache.Remove(id);
        }
    }
}
