using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class ValidationItem
    {
        /// <summary>
        /// Used for the error message
        /// </summary>
        public string Name;

        public string ErrorMessage;

        public Control Control;
        public Control ControlTwo = null;
        public ValidationType Type;

        // for compare validator
        public String ValueToCompare = null;
        public ValidationCompareOperator Operator = ValidationCompareOperator.Equal;

        public bool IsTextBox()
        {
            return typeof(TextBox) == Control.GetType();
        }

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }

    	public bool IsDropDownList()
    	{
			return typeof(DropDownList) == Control.GetType();
    	}
    }
}
