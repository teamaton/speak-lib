using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.TrueOrFalse;
using SpeakFriend.Utils.Web;
using Web.TrueOrFalse.Code;

namespace Web.TrueOrFalse.Admin
{
    public partial class WebForm1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptQuestions.ItemDataBound += rptQuestions_ItemDataBound;

            PopulatePage();
        }

        private void PopulatePage()
        {
            var questions = _questionService.GetAll();
            rptQuestions.DataSource = questions;
            rptQuestions.DataBind();
        }

        void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!ItemTemplateHelper.IsContentItem(e.Item))
                return;

            var itemHelper = new ItemTemplateHelper(e.Item);
            var question = (Question) e.Item.DataItem;

            itemHelper.Find<Label>("lblText").Text = question.Text;
            itemHelper.Find<Label>("lblAnswer").Text = question.Answer.Value;

        }
    }
}
