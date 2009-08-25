using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class OrderByService
    {
        public OrderByService RegisterButton(LinkButton button, OrderBy orderBy)
        {
            button.Click += delegate { ButtonClicked(orderBy); };
            SetCssClass(button, orderBy);
            Changed += delegate { SetCssClass(button, orderBy); };
            return this;
        }
 
        private void ButtonClicked(OrderBy orderBy)
        {
            orderBy.AscOrToggle();
            OnChanged(EventArgs.Empty);
        }

        private void SetCssClass(LinkButton button, OrderBy orderBy)
        {
            button.CssClass = "";
            if (orderBy.IsCurrent()) button.AddCssClass("current");
            button.AddCssClass(orderBy.IsAsc() ? "sort-up" : "sort-down");
        }

        private void OnChanged(EventArgs e)
        {
            if (Changed != null) Changed(this, e);
        }

        public event EventHandler Changed;
    }
}
