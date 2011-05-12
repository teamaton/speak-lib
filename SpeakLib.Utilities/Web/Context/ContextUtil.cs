using System.Web;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	public static class ContextUtil
	{
		public static bool IsLocal
		{
			get { return IsWebContext && HttpContext.Current.Request.IsLocal; }
		}

		public static bool IsWebContext
		{
			get { return HttpContext.Current != null; }
		}
	}
}