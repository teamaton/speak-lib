using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampingInfo.Core
{
    public class ValidationMessage
    {
        public string FieldName;

        public static ValidationMessage MandatoryField(string fieldName)
        {
            return new ValidationMessage { FieldName = fieldName };
        }
    }
}
