using SpeakFriend.Utilities;

namespace SpeakFriend.FileUpload
{
    internal class Settings : AppSettings
    {
    	public static string FileUploadTempDirRelative
    	{
    		get { return Get<string>("FileUploadTempDirRelative"); }
    	}

    	public static string FileUploadTempDirAbsolute
    	{
    		get { return GetAbsolute(FileUploadTempDirRelative); }
    	}
    }
}
