using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class ValidationGroup
    {
        private readonly ValidationService _validationService;

        private readonly ValidationItemList _items = new ValidationItemList();
        private readonly ValidatorBuilder _validatorBuilder;

        private readonly string _groupIdentifier;
        public string GroupIdentifier { get { return _groupIdentifier; } }

        public bool IsFinished = false;

        public ValidationGroup(ValidationService validationService, string validationGroupId)
        {
            _validationService = validationService;
            _groupIdentifier = validationGroupId;
            _validatorBuilder = new ValidatorBuilder(_groupIdentifier);
        }

        public ValidationGroup Register(TextBox textBox)
        {
            _items.Add(new ValidationItem { Control = textBox });
            return this;
        }

        public ValidationGroup RegisterCompare(TextBox toValidate, TextBox toCompare)
        {
            _items.Add(new ValidationItem { Control = toValidate, ControlTwo = toCompare, Type = ValidationType.Compare });
            return this;
        }

        public ValidationGroup RegisterDdl(DropDownList ddl, string valueNotToEqual)
        {
            _items.Add(new ValidationItem
                           {
                               Control = ddl,
                               Type = ValidationType.Compare,
                               ValueToCompare = valueNotToEqual,
                               Operator = ValidationCompareOperator.NotEqual
                           });
            return this;
        }

        public ValidationGroup IsMandatory()
        {
            _items.Last().Type = ValidationType.RequiredField;
            return this;
        }

        public ValidationGroup NameIs(string name)
        {
            _items.Last().Name = name;
            return this;
        }

        public ValidationGroup ErrorMessage(string errorMessage)
        {
            _items.Last().ErrorMessage = errorMessage;
            return this;
        }

        public ValidationGroup IsEmail()
        {
            _items.Last().Type = ValidationType.Email;
            return this;
        }

        public ValidationGroup RegisterButton(Button buttonCreate)
        {
            buttonCreate.ValidationGroup = _groupIdentifier;
            return this;
        }

        public ValidationService FinishGroup()
        {
            foreach (var item in _items)
            {
                if (item.IsTextBox())
                {
                    if (item.Type == ValidationType.RequiredField)
                        _validationService.AddRequiredFieldValidator(_validatorBuilder, item);

                    if (item.Type == ValidationType.Email)
                        _validationService.AddRegularExpressionValidator(_validatorBuilder, item);

                    if (item.Type == ValidationType.Compare)
                        _validationService.AddCompareValidator(_validatorBuilder, item);
                }
            }

            IsFinished = true;

            return _validationService;
        }
    }
}
