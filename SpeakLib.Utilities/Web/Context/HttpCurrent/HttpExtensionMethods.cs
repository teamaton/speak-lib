using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
	public static class ResponseHelper
	{
		public static void RedirectToSamePage(this HttpResponse response, HttpRequest request)
		{
			response.Redirect(request.RawUrl);
		}
	}
}
