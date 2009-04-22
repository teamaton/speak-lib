using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.Web.Utilities.UserControls
{
    public partial class Message : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

		}

		public void ShowSuccess(UserMessage userMessage)
		{
			MakeLookLikeSuccess();
			ShowMessage(userMessage);
		}

		public void ShowSuccess(string title)
		{
			ShowSuccess(new UserMessage(title));
		}

		public void ShowSuccess(string title, string message)
		{
			ShowSuccess(new UserMessage(title, message));
		}

		public void ShowError(UserMessage userMessage)
		{
			MakeLookLikeFailure();
			ShowMessage(userMessage);
		}

		public void ShowError(string title)
		{
			ShowError(new UserMessage(title));
		}

		public void ShowError(string title, string message)
		{
			ShowError(new UserMessage(title, message));
		}

		private void ShowMessage(UserMessage userMessage)
		{
			ltlTitle.Text = userMessage.Title;

			if (userMessage.Messages.Count == 1)
			{
				ltlMsg.Text = userMessage.Messages[0].Text;
				return;
			}

			var msgList = new BulletedList();

			foreach (UserMessageItem msg in userMessage.Messages)
			{
				var listItem = new ListItem(msg.Text);
				msgList.Items.Add(listItem);
			}

			plhMsgs.Controls.Add(msgList);
		}

		private void MakeLookLikeSuccess()
		{
			pnlUserMsg.Visible = true;
			pnlUserMsg.CssClass = "user-message success";
			imgSuccess.Visible = true;
			animSuccess.Enabled = true;
			animError.Enabled = false;
		}

		private void MakeLookLikeFailure()
		{
			pnlUserMsg.Visible = true;
			pnlUserMsg.CssClass = "user-message failure";
			imgError.Visible = true;
			animError.Enabled = true;
			animSuccess.Enabled = false;
		}

		public void Hide()
		{
			pnlUserMsg.Visible = false;
		}
    }
}