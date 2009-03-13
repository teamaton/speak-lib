using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SpeakFriend.Utilities
{
    public static class WebScreenshotGenerator
    {
        public static Bitmap GetScreenshot(Uri uri)
        {
            return GetScreenshot(uri, 0, 0);
        }

        public static Bitmap GetScreenshot(Uri uri, int width, int height)
        {
            Bitmap bitmap;

            using (var webBrowser = new WebBrowser())
            {
                webBrowser.ScrollBarsEnabled = false;
                webBrowser.ScriptErrorsSuppressed = true;

                webBrowser.Navigate(uri);

                while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }


                webBrowser.Width = width > 0 
                    ? width 
                    : webBrowser.Document.Body.ScrollRectangle.Width;


                webBrowser.Height = height > 0 
                    ? height 
                    : webBrowser.Document.Body.ScrollRectangle.Height;

                if (webBrowser.Height <= 0)
                    webBrowser.Height = 768;

                bitmap = new Bitmap(webBrowser.Width, webBrowser.Height);
                webBrowser.DrawToBitmap(bitmap,
                                        new Rectangle(0, 0, webBrowser.Width,
                                                      webBrowser.Height));
            }

            return bitmap;
        }
    }
}
