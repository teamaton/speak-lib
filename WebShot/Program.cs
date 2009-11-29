using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using SpeakFriend.Utilities;

namespace SpeakFriend.WebShot
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("parameters: url, path");
                return;
            }

            var uri = new Uri(args[0]);
            var path = args[1];

            var bitmap = WebScreenshotGenerator.GetScreenshot(uri);

            bitmap.Save(path,ImageFormat.Png);
        }
    }
}
