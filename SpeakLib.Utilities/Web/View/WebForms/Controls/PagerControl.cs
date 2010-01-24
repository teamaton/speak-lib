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

        public event EventHandler PageChanged;

        protected void OnPageChanged(EventArgs e)
        {
            if (PageChanged != null)
                PageChanged(this, e);
        }

        private int pageNavigationRange = 2;
        public int PageNavigationRange
        {
            get { return pageNavigationRange; }
            set { pageNavigationRange = value; }
        }

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
                    OnPageChanged(EventArgs.Empty);
                    break;

                case "PreviousPage":
                    _pager.PreviousPage();
                    OnPageChanged(EventArgs.Empty);
                    break;

                case "NextPage":
                    _pager.NextPage();
                    OnPageChanged(EventArgs.Empty);
                    break;
            }

            CreateChildControls();
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

        public class PageNumberTemplateContainer : WebControl, INamingContainer
        {
            public int PageNumber { get; set; }

            protected override void Render(HtmlTextWriter writer)
            {
                foreach (var child in Controls.OfType<Control>())
                    child.RenderControl(writer);
            }
        }

        [
            Browsable(false),
            PersistenceMode(PersistenceMode.InnerProperty),
            TemplateContainer(typeof (PageNumberTemplateContainer))
        ]
        public virtual ITemplate PageNumberTemplate { get; set; }

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
            TemplateContainer(typeof(PageNumberTemplateContainer))
        ]
        public virtual ITemplate CurrentPageNumberTemplate { get; set; }

        sealed class DefaultCurrentPageNumberTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var label = new Label();
                label.Font.Bold = true;

                var pageNumberTemplateContainer = container as PageNumberTemplateContainer;
                if (pageNumberTemplateContainer != null)
                    label.Text = pageNumberTemplateContainer.PageNumber.ToString();

                container.Controls.Add(label);
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

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate DisabledPreviousPageTemplate { get; set; }

        sealed class DefaultDisabledPreviousPageTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var literal = new Literal { Text = "Previous" };
                container.Controls.Add(literal);
            }
        }

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate NextPageTemplate { get; set; }

        sealed class DefaultNextPageTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var button = new LinkButton { Text = "Next" };
                container.Controls.Add(button);
            }
        }

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate DisabledNextPageTemplate { get; set; }

        sealed class DefaultDisabledNextPageTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var literal = new Literal { Text = "Next" };
                container.Controls.Add(literal);
            }
        }

        [
        Browsable(false),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate SpacerTemplate { get; set; }

        sealed class DefaultSpacerTemplate : ITemplate
        {
            void ITemplate.InstantiateIn(Control container)
            {
                var literal = new Literal { Text = "..." };
                container.Controls.Add(literal);
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

            CreatePreviousPageControl();
            CreatePageNumberControls();
            CreateNextPageControl();
        }

        private void CreatePreviousPageControl()
        {
            if (PreviousPageTemplate == null) 
                PreviousPageTemplate = new DefaultPreviousPageTemplate();
            if (DisabledPreviousPageTemplate == null)
                DisabledPreviousPageTemplate = new DefaultDisabledPreviousPageTemplate();

            var previousPageTemplateContainer = new PlaceHolder();
            if (_pager.IsFirstPage)
                DisabledPreviousPageTemplate.InstantiateIn(previousPageTemplateContainer);
            else
            {
                PreviousPageTemplate.InstantiateIn(previousPageTemplateContainer);
                foreach (var button in previousPageTemplateContainer.Controls.OfType<IButtonControl>())
                {
                    button.CommandName = "PreviousPage";
                }
            }

            Controls.Add(previousPageTemplateContainer);
        }

        private void CreateNextPageControl()
        {
            if (NextPageTemplate == null) 
                NextPageTemplate = new DefaultNextPageTemplate();
            if (DisabledNextPageTemplate == null) 
                DisabledNextPageTemplate = new DefaultDisabledNextPageTemplate();

            var nextPageTemplateContainer = new PlaceHolder();
            if (_pager.IsLastPage)
                DisabledNextPageTemplate.InstantiateIn(nextPageTemplateContainer);
            else
            {
                NextPageTemplate.InstantiateIn(nextPageTemplateContainer);
                foreach (var button in nextPageTemplateContainer.Controls.OfType<IButtonControl>())
                {
                    button.CommandName = "NextPage";
                }
            }

            Controls.Add(nextPageTemplateContainer);
        }

        private void CreatePageNumberControls()
        {
            var pageAnchor = Math.Min(Math.Max(pageNavigationRange + 2, _pager.CurrentPage),
                                      _pager.PageCount - pageNavigationRange - 1);
            
            if (PageNumberTemplate == null) 
                PageNumberTemplate = new DefaultPageNumberTemplate();
            if (CurrentPageNumberTemplate == null) 
                CurrentPageNumberTemplate = new DefaultCurrentPageNumberTemplate();
            
            for (int number = 1; number <= _pager.PageCount;)
            {
                var pageNumberTemplateContainer = new PageNumberTemplateContainer {PageNumber = number};
                
                if (number == _pager.CurrentPage)
                    CurrentPageNumberTemplate.InstantiateIn(pageNumberTemplateContainer);
                else
                    PageNumberTemplate.InstantiateIn(pageNumberTemplateContainer);
                
                pageNumberTemplateContainer.DataBind();
                
                foreach (var button in pageNumberTemplateContainer.Controls.OfType<IButtonControl>())
                {
                    button.CommandName = "GoToPage";
                    button.CommandArgument = number.ToString();
                }
                
                Controls.Add(pageNumberTemplateContainer);

                if (number < pageAnchor - pageNavigationRange) 
                {
                    number = pageAnchor - pageNavigationRange;
                    if (number != 2) CreateSpacer();
                    continue;
                }

                if (number >= pageAnchor + pageNavigationRange && number < _pager.PageCount) 
                {
                    if(number != _pager.PageCount - 1) CreateSpacer();
                    number = _pager.PageCount;
                    continue;
                }
                
                number++;
            }
        }

        private void CreateSpacer()
        {
            if (SpacerTemplate == null) SpacerTemplate = new DefaultSpacerTemplate();
            var spacerTemplateContainer = new PlaceHolder();
            SpacerTemplate.InstantiateIn(spacerTemplateContainer);
            Controls.Add(spacerTemplateContainer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (var child in Controls.OfType<Control>())
                child.RenderControl(writer);
        }

        public void Update()
        {
            CreateChildControls();
        }
    }
}
