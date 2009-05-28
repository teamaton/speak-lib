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
        private const string _AppDataKey = "sf_performanceTracker";
        private const int _renderListSize = 500;
        private RenderDuration _renderDuration;

        public static RenderDurationFiFo GetAllEntries()
        {
            var result = new AppData()[_AppDataKey];

            if(result == null)
                return new RenderDurationFiFo(_renderListSize);

            return (RenderDurationFiFo)result;
        }

        public PerformanceTracker(HttpApplication globalAsax)
        {
            //if(globalAsax.Response.)
            globalAsax.BeginRequest += globalAsax_BeginRequest;
            globalAsax.EndRequest += globalAsax_EndRequest;
        }

        void globalAsax_BeginRequest(object sender, EventArgs e)
        {
            if (_appData[_AppDataKey] == null)
                _appData[_AppDataKey] = new RenderDurationFiFo(_renderListSize);

            var entries = (RenderDurationFiFo)_appData[_AppDataKey];
             _renderDuration = RenderDurationBuilder.GetNewEntry();
             entries.Add(_renderDuration);
        }

        void globalAsax_EndRequest(object sender, EventArgs e)
        {
            if (_appData[_AppDataKey] == null)
                return;

            _renderDuration.StopsNow();

            HttpContext.Current.Response.Write(_renderDuration.Value);
        }

    }
}
