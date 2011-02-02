using System;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web.Render
{
    public static class ControlRenderer
    {
        /// <summary>
        /// Renders the given UserControls in the given order onto an empty Page object and returns the rendered string.
        /// </summary>
        public static string RenderControl (params string[] pathToUserControl)
        {
            var pageHolder = new Page();
            foreach (var path in pathToUserControl)
            {
                var viewControl = (UserControl)pageHolder.LoadControl(path);
                pageHolder.Controls.Add(viewControl);
            }
            var output = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, output, true);

            return output.ToString();
        }

        public static string RenderControl<T>(string pathToControl, Action<T> modifyControl)
            where T : UserControl
        {
            var page = new Page();
            var ctl = (T)page.LoadControl(pathToControl);

            if (modifyControl != null)
                modifyControl(ctl);

            page.Controls.Add(ctl);
            var writer = new StringWriter();
            HttpContext.Current.Server.Execute(page, writer, true);
            return writer.ToString();
        }

        public static string RemoveHtmlSpace(this string html)
        {
            return Regex.Replace(Regex.Replace(html.Trim(), @"\>\s+\<", "><", RegexOptions.Singleline)
                                 , @"\s+\</", "</", RegexOptions.Singleline);
        }
    }
}