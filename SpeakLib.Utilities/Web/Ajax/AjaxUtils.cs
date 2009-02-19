using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web
{
    public class AjaxUtils
    {
        public static bool IsInPartialRendering(Control control)
        {
            foreach (var updatePanel in control.Controls.OfType<UpdatePanel>())
            {
                if (updatePanel.IsInPartialRendering)
                    return true;
            }
            foreach (var container in control.Controls.OfType<Control>())
            {
                if (IsInPartialRendering(container))
                    return true;
            }
            return false;
        }
    }
}
