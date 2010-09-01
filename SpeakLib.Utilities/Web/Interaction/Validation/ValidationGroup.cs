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
        private readonly ValidationBuilder _validationBuilder;

        private readonly ValidationItemList _items = new ValidationItemList();
        private readonly ValidatorBuilder _validatorBuilder;

        private readonly string _groupIdentifier;
        public string GroupIdentifier { get { return _groupIdentifier; } }

        public bool IsFinished = false;

        public ValidationGroup(ValidationBuilder validationBuilder, ValidationSettings settings, string validationGroupId)
        {
            _validationBuilder = validationBuilder;
            _groupIdentifier = validationGroupId;
            _validatorBuilder = new ValidatorBuilder(_groupIdentifier, settings);
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

        public ValidationGroup AsMandatory()
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

        public ValidationGroup AsPositiveInteger()
        {
            _items.Last().Type = ValidationType.Positive_Integer;
            return this;
        }

        public ValidationGroup AsPrice()
        {
            _items.Last().Type = ValidationType.Price;
            return this;
        }


        public ValidationGroup AsEmail()
        {
            _items.Last().Type = ValidationType.Email;
            return this;
        }

        public ValidationGroup AsGuid()
        {
            _items.Last().Type = ValidationType.GUID;
            return this;
        }

        public ValidationGroup AsUri()
        {
            _items.Last().Type = ValidationType.Uri;
            return this;
        }

        public ValidationGroup RegisterButton(IButtonControl buttonCreate)
        {
            buttonCreate.ValidationGroup = _groupIdentifier;
            return this;
        }

        public ValidationBuilder FinishGroup()
        {
            foreach (var item in _items)
            {
                if (item.IsTextBox() || item.IsDropDownList())
                {
                    switch (item.Type)
                    {
                    	case ValidationType.RequiredField:
                    		_validationBuilder.AddRequiredFieldValidator(_validatorBuilder, item);
                    		break;
                    	case ValidationType.Compare:
                    		_validationBuilder.AddCompareValidator(_validatorBuilder, item);
                    		break;
                    	case ValidationType.Email:
                    		_validationBuilder.AddRegularExpressionValidator(_validatorBuilder, item, ValidationUtil.Regex_Email);
                    		break;
                    	case ValidationType.GUID:
                    		_validationBuilder.AddRegularExpressionValidator(_validatorBuilder, item, ValidationUtil.Regex_GUID);
                    		break;
                    	case ValidationType.Uri:
                    		_validationBuilder.AddRegularExpressionValidator(_validatorBuilder, item, ValidationUtil.Regex_Uri);
                    		break;
                    	case ValidationType.Positive_Integer:
                    		_validationBuilder.AddRegularExpressionValidator(_validatorBuilder, item, ValidationUtil.Regex_PositiveInteger);
                    		break;
                    	case ValidationType.Price:
                    		_validationBuilder.AddRegularExpressionValidator(_validatorBuilder, item, ValidationUtil.Regex_Price);
                    		break;
                    }
                }
            }

            IsFinished = true;

            return _validationBuilder;
        }

    }
}
