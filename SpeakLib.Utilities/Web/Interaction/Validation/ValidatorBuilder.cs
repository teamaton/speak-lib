using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
// http://blogs.msdn.com/mattgi/archive/2007/01/23/asp-net-ajax-validators.aspx 
//using CompareValidator = Sample.Web.UI.Compatibility.CompareValidator;
//using CustomValidator = Sample.Web.UI.Compatibility.CustomValidator;
//using RangeValidator = Sample.Web.UI.Compatibility.RangeValidator;
//using RegularExpressionValidator = Sample.Web.UI.Compatibility.RegularExpressionValidator;
//using RequiredFieldValidator = Sample.Web.UI.Compatibility.RequiredFieldValidator;
//using ValidationSummary = Sample.Web.UI.Compatibility.ValidationSummary;

namespace SpeakFriend.Utilities.Web
{
    public class ValidatorBuilder
    {
        readonly string _groupIdentifier;
        readonly ValidationSettings _settings;

        public ValidatorBuilder(string groupIdentifier, ValidationSettings setting)
        {
            _groupIdentifier = groupIdentifier;
            _settings = setting;
        }

        public ValidatorCalloutExtender GetCalloutExtender(BaseValidator validator, string controlId)
        {
            return new ValidatorCalloutExtender
            {
                ID = controlId,
                TargetControlID = validator.ID,
                HighlightCssClass = _settings.CssClass_CalloutHighlight,
                WarningIconImageUrl = _settings.ImgPath_CalloutWarning
            };
        }

        public BaseValidator GetRegularExpressionValidator(ValidationItem item, string identifier, string regex)
        {
            var validator = new RegularExpressionValidator();

            validator.ID = identifier;
            validator.ValidationExpression = regex;

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
			validator.ControlToCompare = item.ControlTwo != null ? item.ControlTwo.ID : null;
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
