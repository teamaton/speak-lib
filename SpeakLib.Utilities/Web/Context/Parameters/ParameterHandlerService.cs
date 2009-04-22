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

		public void ProcessParam(ParameterHandler parameterHandler)
		{
			ProcessParam(HttpContext.Current.Request.QueryString, parameterHandler);
		}

		public void ProcessParam(NameValueCollection queryParams, ParameterHandler parameterHandler)
		{
			if (parameterHandler.AppliesOnlyLocal && !ContextUtil.IsLocal)
				return;

			parameterHandler.Action(queryParams[parameterHandler.Name]);
		}

        public bool IsHandlerActive(ParameterHandler parameterHandler)
        {
            return IsHandlerActive(HttpContext.Current.Request.QueryString, parameterHandler);
        }

        private bool IsHandlerActive(NameValueCollection queryParams, ParameterHandler parameterHandler)
        {
            var value = queryParams.Get(parameterHandler.Name);

            return !string.IsNullOrEmpty(value);

        }
    }
}
