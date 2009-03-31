using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public static class TextBoxUtils
    {
        public static bool EnsureNotEmpty(TextBox textBox, bool highlightIfNot)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                if (highlightIfNot)
                    SetMissingStyle(textBox);
                return false;
            }

            UnsetMissingStyle(textBox);
            return true;
        }

        public static bool EnsureContainsInt(TextBox textBox, bool highlightIfNot)
        {
            try
            {
                GetIntOrNullOrException(textBox);
                UnsetMissingStyle(textBox);
                return true;
            }
            catch (FormatException)
            {
                if (highlightIfNot)
                    SetMissingStyle(textBox);
                return false;
            }
            catch (OverflowException)
            {
                if (highlightIfNot)
                    SetMissingStyle(textBox);
                return false;
            }
        }

        public static bool EnsureContainsFloat(TextBox textBox, bool highlightIfNot)
        {
            try
            {
                GetFloatOrNullOrException(textBox);
                UnsetMissingStyle(textBox);
                return true;
            }
            catch (FormatException)
            {
                if (highlightIfNot)
                    SetMissingStyle(textBox);
                return false;
            }
            catch (OverflowException)
            {
                if (highlightIfNot)
                    SetMissingStyle(textBox);
                return false;
            }
        }

        private static string MakeParsableForInvariant(string number)
        {
            return number
                .Replace(".", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
                .Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
        }

        /// <summary>
        /// Parses the content of the textbox for an int
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>The int in the box or null (if empty) or an exception if one occurred.</returns>
        public static int? GetIntOrNullOrException(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
                return null;

            var text = MakeParsableForInvariant(textBox.Text);

            return Convert.ToInt32(text, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parses the content of the textbox for a float
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>The float in the box or null (if empty) or an exception if one occurred.</returns>
        public static float? GetFloatOrNullOrException(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
                return null;

            var text = MakeParsableForInvariant(textBox.Text);

            return Convert.ToSingle(text, CultureInfo.InvariantCulture);
        }

        public static void UnsetMissingStyle(WebControl control)
        {
            control.CssClass = control.CssClass.Replace(" missing", "");
        }

        public static void SetMissingStyle(WebControl control)
        {
            control.CssClass += " missing";
        }

        public static void SetMissingStyle(WebControl control, bool set)
        {
            if (set)
                SetMissingStyle(control);
            else
                UnsetMissingStyle(control);
        }

        public static bool EnsureNoHarmfulText(TextBox textBox, bool highlightIfError)
        {
            var text = textBox.Text;

            if (false)
            {
                if (highlightIfError)
                    SetMissingStyle(textBox);
                return false;
            }

            UnsetMissingStyle(textBox);

            return true;
        }
    }
}
