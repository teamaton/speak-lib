using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
	public static class RequestResponseExtensions
	{
		public static void RedirectToSamePage(this HttpResponse response, HttpRequest request)
		{
			response.Redirect(request.RawUrl);
		}

		public static string GetIpAddress(this HttpRequest request)
		{
			var ip = request.UserHostAddress;

			if (!string.IsNullOrEmpty(ip))
				return ip;

			// See: http://forums.asp.net/p/1053767/1496008.aspx

			ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			// If there is no proxy, get the standard remote address

			if (string.IsNullOrEmpty(ip) || (ip.ToLowerInvariant() == "unknown"))
				ip = request.ServerVariables["REMOTE_ADDR"];

			return ip ?? "unknown";
		}
	}
}
