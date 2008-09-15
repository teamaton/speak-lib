using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.TrueOrFalse;
using Web.TrueOrFalse.Code;

namespace Web.TrueOrFalse.Admin
{
    public partial class WebForm2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            var question = new Question();
            question.Answer.Value = txtAnswer.Text;
            question.Text = txtQuestion.Text;

            _questionService.Create(question);
        }
    }
}
