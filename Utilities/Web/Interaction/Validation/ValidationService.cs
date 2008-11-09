﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace SpeakFriend.Utilities.Web
{
    public class ValidationService
    {
        private bool _isPageRegistered;
        private bool _isPlaceHolderRegistered;

        private static int _validationGroupId = 1;
        private static string NextGroupId { get { return "vg_" + _validationGroupId++; } }

        private static int _validatorControlId = 1;
        private static string NextValidationControlId { get { return "vd_" + _validatorControlId++; } }

        private Page _page;
        private PlaceHolder _messagePlaceHolder;
        private ValidationGroup _lastCreatedValidationGroup;

        public readonly List<BaseValidator> Validators = new List<BaseValidator>();

        public ValidationService Register(Page page)
        {
            if (_isPageRegistered)
                throw new Exception("Only one Page can be registered!");

            _page = page;
            _isPageRegistered = true;
            return this;
        }

        public ValidationService RegisterMessage(PlaceHolder placeHolder)
        {
            if (_isPlaceHolderRegistered)
                throw new Exception("Only one PlaceHolder can be registered!");

            _messagePlaceHolder = placeHolder;
            _messagePlaceHolder.Controls.Add(
                new Panel { ID = "div", CssClass = "validatorCallout" });
            _isPlaceHolderRegistered = true;
            return this;
        }

        public ValidationGroup RegisterButton(Button groupButton)
        {
            _lastCreatedValidationGroup = new ValidationGroup(this, NextGroupId).RegisterButton(groupButton);
            return _lastCreatedValidationGroup;
        }

        public void Finish()
        {
            if (!_lastCreatedValidationGroup.IsFinished)
                _lastCreatedValidationGroup.FinishGroup();

            EnsureThatNecessaryControlsAreRegistered();
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

        internal void AddRegularExpressionValidator(ValidatorBuilder validatorBuilder, ValidationItem item)
        {
            var validator = validatorBuilder.GetRegularExpressionValidator(item, NextValidationControlId);
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

        private void AddToForm(BaseValidator validator, ValidatorCalloutExtender calloutExtender)
        {
            _messagePlaceHolder.FindControl("div").Controls.Add(validator);
            _messagePlaceHolder.FindControl("div").Controls.Add(calloutExtender);
        }

        public bool IsValid()
        {
            return _page.IsValid;
        }
    }    
}
