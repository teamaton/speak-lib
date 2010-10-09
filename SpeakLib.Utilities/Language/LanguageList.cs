using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class LanguageList : List<Language>
    {
        public LanguageList(){}

        public LanguageList(IEnumerable<Language> list)
        {
            AddRange(list);
        }

        public Language GetByIso2(string iso2)
        {
            foreach (Language language in this)
                if (language.Iso2.Equals(iso2, StringComparison.InvariantCultureIgnoreCase))
                    return language;

            return null;
        }

        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        public string ToString(string delimiter)
        {
            var builder = new StringBuilder();

            foreach (Language language in this)
                builder.Append(language + delimiter);

            if (builder.Length > 0)
                builder.Length -= delimiter.Length;

            return builder.ToString();
        }

        public string ToNamesString()
        {
            return ToNamesString(", ");
        }

        public string ToNamesString(string delimiter)
        {
            var builder = new StringBuilder();

            foreach (Language language in this)
                builder.Append(language.Name + delimiter);

            if (builder.Length > 0)
                builder.Length -= delimiter.Length;

            return builder.ToString();
        }


        public LanguageList GetByIso2(string[] iso2List)
        {
            return new LanguageList(FindAll(lang => iso2List.ToList().Contains(lang.Iso2)));
        }

    	public LanguageList SortByName()
    	{
    		Sort((l1, l2) => l1.Name.CompareTo(l2.Name));
    		return this;
    	}
    }
}
