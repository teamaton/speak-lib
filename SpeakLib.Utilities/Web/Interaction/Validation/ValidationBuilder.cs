using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class ValidationBuilder
    {
        private bool _isPageRegistered;
        private bool _isPlaceHolderRegistered;

        private static int _validationGroupId = 1;
        private static string NextGroupId { get { return "vg_" + _validationGroupId++; } }

        private static int _validatorControlId = 1;
        private static string NextValidationControlId { get { return "vd_" + _validatorControlId++; } }

        private Page _page;
        private Control _messagePlaceHolder;
        private ValidationGroup _lastCreatedValidationGroup;

        public readonly List<BaseValidator> Validators = new List<BaseValidator>();

        private readonly ValidationSettings _validationSettings;
        
        public ValidationGroup CurrentValidationGroup{get { return _lastCreatedValidationGroup; }}

        public ValidationBuilder()
        {
            _validationSettings = new ValidationSettings
                                  	{
                                  		CssClass_Callout = "validatorCallout",
                                  		CssClass_CalloutHighlight = "validatorCallout-highlight",
                                  		ImgPath_CalloutWarning = "/style/img/warning-large.gif"
                                  	};
        }

        public ValidationBuilder(ValidationSettings settings)
        {
            _validationSettings = settings;
        }

		/// <summary>
		/// Static factory method for more concise initialization.
		/// </summary>
		public static ValidationGroup Create(Page page, Control placeHolder, IButtonControl button)
		{
			return new ValidationBuilder().Register(page).RegisterPlaceHolder(placeHolder).RegisterButton(button);
		}

        public ValidationBuilder Register(Page page)
        {
            if (_isPageRegistered)
                throw new Exception("Only one Page can be registered!");

            _page = page;
            _isPageRegistered = true;
            return this;
        }

        public ValidationBuilder RegisterPlaceHolder(Control placeHolder)
        {
            if (_isPlaceHolderRegistered)
                throw new Exception("Only one placeholder Control can be registered!");

            _messagePlaceHolder = placeHolder;
            _messagePlaceHolder.Controls.Add(
                new Panel { ID = "div", CssClass = _validationSettings.CssClass_Callout });
            _isPlaceHolderRegistered = true;
            return this;
        }

        public ValidationGroup RegisterButton(IButtonControl groupButton)
        {
            _lastCreatedValidationGroup = new ValidationGroup(this, _validationSettings, NextGroupId).RegisterButton(groupButton);
            return _lastCreatedValidationGroup;
        }

        public ValidationBuilder Finish()
        {
            if (!_lastCreatedValidationGroup.IsFinished)
                _lastCreatedValidationGroup.FinishGroup();

            EnsureThatNecessaryControlsAreRegistered();

        	return this;
        }

        private void EnsureThatNecessaryControlsAreRegistered()
        {
            if (!_isPageRegistered)
                throw new Exception("No Page was registered!");

            if (!_isPlaceHolderRegistered)
                throw new Exception("No PlaceHolder was registered!");
        }

        internal void AddCompareValidator(ValidatorBuilder validatorBuilder, ValidationItem item)
        {
            var validator = validatorBuilder.GetCompareValidator(item, NextValidationControlId);
            Validators.Add(validator);
            var calloutExtender = validatorBuilder.GetCalloutExtender(validator, NextValidationControlId);
            AddToForm(validator, calloutExtender);
        }

        internal void AddRegularExpressionValidator(ValidatorBuilder validatorBuilder, ValidationItem item, string regex)
        {
            var validator = validatorBuilder.GetRegularExpressionValidator(item, NextValidationControlId, regex);
            Validators.Add(validator);
            var calloutExtender = validatorBuilder.GetCalloutExtender(validator, NextValidationControlId);
            AddToForm(validator, calloutExtender);
        }

        internal void AddRequiredFieldValidator(ValidatorBuilder validatorBuilder, ValidationItem item)
        {
            var validator = validatorBuilder.GetRequiredFieldValidator(item, NextValidationControlId);
            Validators.Add(validator);
            var calloutExtender = validatorBuilder.GetCalloutExtender(validator, NextValidationControlId);
            AddToForm(validator, calloutExtender);
        }

        private void AddToForm(Control validator, Control calloutExtender)
        {
            _messagePlaceHolder.FindControl("div").Controls.Add(validator);
            _messagePlaceHolder.FindControl("div").Controls.Add(calloutExtender);
        }

        public bool IsValid()
        {
            return _page.IsValid;
        }

        public UserMessage GetUserMessage()
        {
            var userMessage = new UserMessage();
            foreach (var validator in Validators)
            {
                validator.Validate();
                if (!validator.IsValid)
                    userMessage.AddItem(validator.ErrorMessage);
            }

            return userMessage;
        }
    }    
}
