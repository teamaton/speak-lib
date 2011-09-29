using System.Collections.Specialized;
using System.Linq;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	public class ParameterHandlerService
	{
		protected IHttpCurrent _httpCurrent;

		protected IRequest _request
		{
			get { return _httpCurrent.Request; }
		}

		protected IResponse _response
		{
			get { return _httpCurrent.Response; }
		}

		public ParameterHandlerService(IHttpCurrent httpCurrent)
		{
			_httpCurrent = httpCurrent;
		}

		public void ProcessParams(ParameterHandlerList parameterHandlers)
		{
			ProcessParams(_request.QueryString, parameterHandlers);
		}

		/// <summary>
		/// Processes all given parameters.
		/// </summary>
		/// <param name="queryParams"></param>
		/// <param name="parameterHandlers"></param>
		public void ProcessParams(NameValueCollection queryParams, ParameterHandlerList parameterHandlers)
		{
			foreach (var parameterHandler in parameterHandlers)
			{
				if (queryParams.AllKeys.Contains(parameterHandler.Name))
				{
					if (parameterHandler.AppliesOnlyLocal && !ContextUtil.IsLocal)
						continue;

					parameterHandler.Action(queryParams[parameterHandler.Name]);
				}
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
		/// True if query string contains a parameter handled by a registered <see cref="ParameterHandler">ParamaterHandler</see>.
		/// </summary>
		/// <param name="parameterHandler"></param>
		/// <returns></returns>
		public bool DoesHandlerExist(ParameterHandler parameterHandler)
		{
			return DoesHandlerExist(_request.QueryString, parameterHandler);
		}

		/// <summary>
		/// True if query string contains a parameter handled by a registered <see cref="ParameterHandler">ParamaterHandler</see>.
		/// </summary>
		/// <param name="queryParams"></param>
		/// <param name="parameterHandler"></param>
		/// <returns></returns>
		private bool DoesHandlerExist(NameValueCollection queryParams, ParameterHandler parameterHandler)
		{
			var value = queryParams.Get(parameterHandler.Name);

			return !string.IsNullOrEmpty(value);
		}
	}
}