using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public class Answer
    {
        private int _value;

        public int IntValue
        {
            get { return _value; }
            set { this._value = value; }
        }

        public string GeneralStringValue
        {
            get { return _value.ToString(); }

            /// <remarks>Hier soll später entschieden werden,
            /// von welchem Datentyp die Antwort ist (Datentypweiche) und
            /// ein entprechendes Feld Typ o.ä. belegt werden.</remarks>
            set { this._value = Int32.Parse(value); }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
