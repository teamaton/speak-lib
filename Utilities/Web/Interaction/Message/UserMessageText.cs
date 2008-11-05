using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class UserMessageText
    {
        private const string _thankYou = "Vielen Dank.";

        private const string _admin_ErrorCheckInput = "Es ist ein Fehler aufgetreten. Bitte prüfen Sie Ihre Eingabe.";
        private const string _admin_ResetInput = "Ihre Eingaben wurden zurückgesetzt.";
        private const string _admin_DetailsSavedSuccessfully = "Ihre Angaben wurden erfolgreich gespeichert.";

        public string AdminSaveSuccess()
        {
            return _admin_DetailsSavedSuccessfully;
        }

        public string ThankYou()
        {
            return _thankYou;
        }

        public string InputError()
        {
            return _admin_ErrorCheckInput;
        }

        public string InputReseted()
        {
            return _admin_ResetInput;
        }
    }
}
