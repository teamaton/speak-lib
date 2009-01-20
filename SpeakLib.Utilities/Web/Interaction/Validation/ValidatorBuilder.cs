using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace SpeakFriend.Utilities.Web
{
    public class ValidatorBuilder
    {
        public const string EmailRegex =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private readonly string _groupIdentifier;

        public ValidatorBuilder(string groupIdentifier)
        {
            _groupIdentifier = groupIdentifier;
        }

        public ValidatorCalloutExtender GetCalloutExtender(BaseValidator validator, string controlId)
        {
            return new ValidatorCalloutExtender
            {
                ID = controlId,
                TargetControlID = validator.ID,
                HighlightCssClass = "validatorCallout-highlight",
                WarningIconImageUrl = "/style/img/warning-large.gif"
            };
        }

        public BaseValidator GetRegularExpressionValidator(ValidationItem item, string identifier)
        {
            var validator = new RegularExpressionValidator();

            validator.ID = identifier;
            validator.ValidationExpression = EmailRegex;

            SetSharedValues(validator, item);

            return validator;
        }

        public BaseValidator GetRequiredFieldValidator(ValidationItem item, string identifier)
        {
            var validator = new RequiredFieldValidator();
            validator.ID = identifier;

            SetSharedValues(validator, item);

            return validator;
        }

        public BaseValidator GetCompareValidator(ValidationItem item, string identifier)
        {
            var validator = new CompareValidator();
            validator.ID = identifier;

            SetSharedValues(validator, item);
            validator.ControlToCompare = item.ControlTwo.ID;
            validator.Operator = item.Operator;
            validator.ValueToCompare = item.ValueToCompare;

            return validator;
        }

        public void SetSharedValues(BaseValidator validator, ValidationItem item)
        {
            validator.ControlToValidate = item.Control.ID;
            validator.ValidationGroup = _groupIdentifier;
            validator.EnableClientScript = true;
            validator.Display = ValidatorDisplay.None;
            validator.ErrorMessage = item.GetErrorMessage();
        }
    }
}
