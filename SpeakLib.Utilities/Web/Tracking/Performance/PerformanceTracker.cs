using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class PerformanceTracker
    {
        private readonly AppData _appData = new AppData();
        private string _AppDataKey = "performanceTracker_" + Guid.NewGuid();

        public PerformanceTracker(HttpApplication globalAsax)
        {
            globalAsax.BeginRequest += globalAsax_BeginRequest;
            globalAsax.EndRequest += globalAsax_EndRequest;
        }

        void globalAsax_BeginRequest(object sender, EventArgs e)
        {
            if (_appData[_AppDataKey] == null)
                _appData[_AppDataKey] = new RenderDurationFiFo(500);

            var entries =  _appData[_AppDataKey] as RenderDurationFiFo;
            entries.Add(RenderDurationBuilder.GetEntry());
        }

        void globalAsax_EndRequest(object sender, EventArgs e)
        {
            
        }

    }
}
