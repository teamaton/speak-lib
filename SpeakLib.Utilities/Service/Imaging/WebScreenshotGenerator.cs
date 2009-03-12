﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpeakFriend.Utilities
{
    public static class WebScreenshotGenerator
    {
        public static Bitmap GetScreenshot(Uri uri)
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

                webBrowser.Width = webBrowser.Document.Body.ScrollRectangle.Width;
                webBrowser.Height = webBrowser.Document.Body.ScrollRectangle.Height;

                if (webBrowser.Height <= 0)
                    webBrowser.Height = 768;

                bitmap = new Bitmap(webBrowser.Width, webBrowser.Height);
                webBrowser.DrawToBitmap(bitmap, new Rectangle(0, 0, webBrowser.Width, webBrowser.Height));
            }

            return bitmap;
        }
    }
}
