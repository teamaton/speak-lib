using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;

namespace SpeakFriend.Utilities.Web.Webservice
{
	public static class WebserviceExtensions
	{
		/// <summary>
		/// Returns a UTF-8 encoded MemoryStream from the given string that can be used as a response to an HTTP request.
		/// </summary>
		public static Stream ToResponseStream(this string output)
		{
			if (WebOperationContext.Current != null)
				WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";

			//trick to output exactly what you want (without wcf wrapping it)
			return new MemoryStream(Encoding.UTF8.GetBytes(output));
		}

		public static void SetNoCacheability()
		{
			if (WebOperationContext.Current != null)
				WebOperationContext.Current.OutgoingResponse.Headers.Add("Cache-Control", "no-cache, max-age=0");
		}

		public static void SetStatusCodeNoContent()
		{
			if (WebOperationContext.Current != null)
				WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;
		}
	}
}
