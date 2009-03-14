using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class ParameterHandlerService
    {
        public void ProcessGlobalParams(ParameterHandlerList parameterHandlers)
        {
            ProcessGlobalParams(HttpContext.Current.Request.QueryString, parameterHandlers);
        }

        /// <summary>
        /// Processes all given parameters.
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="parameterHandlers"></param>
        public void ProcessGlobalParams(NameValueCollection queryParams, ParameterHandlerList parameterHandlers)
        {
            foreach (string itemKey in queryParams.Keys)
                if (parameterHandlers.Contains(itemKey))
                {
                    var handler = parameterHandlers.GetByName(itemKey);

                    if (handler.AppliesOnlyLocal && !ContextUtil.IsLocal)
                        continue;

                    handler.Action(queryParams[itemKey]);
                }
        }
    }
}
