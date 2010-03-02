using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public static class PathUtil
    {
        public static string GetRelativePath(string path, string rootDirectory)
        {
            return path.Replace(rootDirectory + Path.DirectorySeparatorChar, "");
        }

    	/// <summary>
    	/// Ensures that all folders on the given path - file or dir (dir must end with /) - exist;
    	/// if they don't they will be created.<br/>
    	/// Throws a <see cref="SpeakLibException"/> if the missing folders cannot be created.
    	/// </summary>
    	/// <param name="absolutePath">Absolute path of a file or directory (dir must end with /).</param>
    	public static void EnsureDirectoryExists(this string absolutePath)
    	{
    		if (String.IsNullOrEmpty(absolutePath))
    			return;

    		var dirPath = Path.GetDirectoryName(absolutePath);

    		if (String.IsNullOrEmpty(dirPath))
    			return;

    		try
    		{
    			if (!Directory.Exists(dirPath))
    				Directory.CreateDirectory(dirPath);
    		}
    		catch (IOException ioe)
    		{
    			throw new SpeakLibException(String.Format("Path '{0}' could not be created!", dirPath), ioe);
    		}

    		if (!Directory.Exists(dirPath))
    			throw new SpeakLibException(String.Format("Path '{0}' could not be created!", dirPath));
    	}
    }
}
