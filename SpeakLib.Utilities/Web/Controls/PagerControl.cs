using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
    [ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class PagerControl : CompositeControl
    {
        private IPager _pager;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EnsureChildControls();

            foreach (var container in Controls.OfType<Control>())
                RegisterButtonEvents(container, Button_Command);
        }

        void Button_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "GoToPage":
                    _pager.CurrentPage = Convert.ToInt32(e.CommandArgument);
                    break;

                case  "PreviousPage":
                    _pager.PreviousPage();
                    break;
            }
        }

        public static void RegisterButtonEvents(Control container, CommandEventHandler eventHandler)
        {
            foreach (var button in container.Controls.OfType<IButtonControl>())
                button.Command += eventHandler;
        }


        public void Register(IPager pager)
        {
            _pager = pager;
            CreateChildControls();
        }

        [
            Browsable(false),
            PersistenceMode(PersistenceMode.InnerProperty),
            TemplateContainer(typeof (PageNumberTemplateContainer))
        ]
        public virtual ITemplate PageNumberTemplate { get; set; }

        public class PageNumberTemplateContainer : WebControl, INamingContainer
        {
            public int PageNumber { get; set; }

            protected override void Render(HtmlTextWriter writer)
            {
                foreach (var child in Controls.OfType<Control>())
                    child.RenderControl(writer);
            }
        }

        sealed class DefaultPageNumberTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var button = new LinkButton();

                var pageNumberTemplateContainer = container as PageNumberTemplateContainer;
                if (pageNumberTemplateContainer != null)
                    button.Text = pageNumberTemplateContainer.PageNumber.ToString();

                container.Controls.Add(button);
            }
        }

        [
            Browsable(false),
            PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate PreviousPageTemplate { get; set; }

        sealed class DefaultPreviousPageTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var button = new LinkButton { Text = "Previous" };
                container.Controls.Add(button);
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (_pager == null) return;

            if (PreviousPageTemplate == null) PreviousPageTemplate = new DefaultPreviousPageTemplate();
            var previousPageTemplateContainer = new PlaceHolder();
            PreviousPageTemplate.InstantiateIn(previousPageTemplateContainer);
            foreach (var button in previousPageTemplateContainer.Controls.OfType<IButtonControl>())
            {
                button.CommandName = "PreviousPage";
            }
            Controls.Add(previousPageTemplateContainer);
            
            if (PageNumberTemplate == null) PageNumberTemplate = new DefaultPageNumberTemplate();
            for (int number = 1; number <= _pager.PageCount; number++)
            {
                var pageNumberTemplateContainer = new PageNumberTemplateContainer {PageNumber = number};
                PageNumberTemplate.InstantiateIn(pageNumberTemplateContainer);
                pageNumberTemplateContainer.DataBind();
                foreach (var button in pageNumberTemplateContainer.Controls.OfType<IButtonControl>())
                {
                    button.CommandName = "GoToPage";
                    button.CommandArgument = number.ToString();
                }
                Controls.Add(pageNumberTemplateContainer);
            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (var child in Controls.OfType<Control>())
                child.RenderControl(writer);
        }
    }
}
