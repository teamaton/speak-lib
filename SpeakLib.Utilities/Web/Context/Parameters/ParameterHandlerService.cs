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
        private IHttpCurrent _httpCurrent;

        private IRequest _request { get { return _httpCurrent.Request; } }
        private IResponse _response { get { return _httpCurrent.Response; } }

        public ParameterHandlerService(IHttpCurrent httpCurrent)
        {
            _httpCurrent = httpCurrent;
        }

        public void ProcessGlobalParams(ParameterHandlerList parameterHandlers)
        {
            ProcessGlobalParams(_request.QueryString, parameterHandlers);
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
			ProcessParam(_request.QueryString, parameterHandler);
		}

		public void ProcessParam(NameValueCollection queryParams, ParameterHandler parameterHandler)
		{
			if (parameterHandler.AppliesOnlyLocal && !ContextUtil.IsLocal)
				return;

			parameterHandler.Action(queryParams[parameterHandler.Name]);
		}


        /// <summary>
        /// True if query string contains a paramter handeld by an registered <see cref="ParameterHandler">ParamaterHandler</see>
        /// </summary>
        /// <param name="parameterHandler"></param>
        /// <returns></returns>
        public bool DoesApply(ParameterHandler parameterHandler)
        {
            return DoesApply(_request.QueryString, parameterHandler);
        }

        /// <summary>
        /// True if query string contains a paramter handeld by an registered <see cref="ParameterHandler">ParamaterHandler</see>
        /// </summary>
        /// <param name="queryParams"></param>
        /// <param name="parameterHandler"></param>
        /// <returns></returns>
        private bool DoesApply(NameValueCollection queryParams, ParameterHandler parameterHandler)
        {
            var value = queryParams.Get(parameterHandler.Name);

            return !string.IsNullOrEmpty(value);
        }
    }
}
