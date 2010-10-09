using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.Utilities
{
    public class LanguageService
    {
        private readonly ISession _session;

        public LanguageService(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Removes all items before adding all languages alphabetically by name.
        /// </summary>
        /// <param name="listControl"></param>
        public void Fill(ListControl listControl)
        {
            listControl.Items.Clear();
            FillSorted(listControl);
        }

        /// <summary>
        /// Removes all items before adding all languages alphabetically by name.
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="firstItem"></param>
        public void Fill(ListControl listControl, ListItem firstItem)
        {
            listControl.Items.Clear();
            listControl.Items.Add(firstItem);
            FillSorted(listControl);
        }

        private void FillSorted(ListControl listControl)
        {
            var allLangs = GetAll();
            var langsSorted = allLangs.OrderBy(lang => lang.Name);
            foreach (Language language in langsSorted)
                listControl.Items.Add(
                    new ListItem(language.Name, language.Iso2));
        }

        public LanguageList GetAll()
        {
        	return new LanguageList(_session.CreateCriteria(typeof (Language)).List<Language>());

			//if (Cache.Get<LanguageList>(CacheKeys.Languages) == null)
			//{
			//    var list = _session.CreateCriteria(typeof (Language))
			//        .List<Language>();

			//    Cache.Add(CacheKeys.Languages, new LanguageList(list));
			//}

			//return Cache.Get<LanguageList>(CacheKeys.Languages);
        }

		/// <summary>
		/// Does not use the application cache.
		/// </summary>
        public Language GetByIso2(string iso2)
        {
			return GetAll().GetByIso2(iso2);
			//return _session.Get<Language>(iso2.ToUpperInvariant());
        }

        public LanguageList GetByIso2(string[] iso2List)
        {
            return GetAll().GetByIso2(iso2List);
        }

        public Language GetGerman()
        {
            return GetAll().GetByIso2("de");
        }

        public Language GetEnglish()
        {
            return GetAll().GetByIso2("en");
        }

        public Language GetSpanish()
        {
            return GetAll().GetByIso2("es");
        }

        public Language GetItalian()
        {
            return GetAll().GetByIso2("it");
        }

        public Language GetDutch()
        {
            return GetByIso2("nl");
        }

        public Language GetFrench()
        {
            return GetByIso2("fr");
		}

		public Language GetPolish()
		{
			return GetByIso2("pl");
		}

        public Language GetByName(string name)
        {
            return GetAll().Find(lang => lang.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Returns a Language object from the subdomain of the given URL. Default is German.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Language GetFromUrl(Uri url)
        {
            var langIso2 = UriUtils.FirstSubdomainNotWww(url);
            var language = GetByIso2(langIso2);

            return language ?? GetGerman();
        }

    	public Language GetByCountryIso2(string countryIso2)
    	{
    		return GetByIso2(GetLangIso2FromCountryIso2(countryIso2));
    	}

    	/// <summary>
        /// Returns the Iso2 code of the language that has been assigned as default for the
        /// country with the given Iso2 code.
        /// </summary>
        /// <param name="countryIso2"></param>
        /// <returns></returns>
        public string GetLangIso2FromCountryIso2(string countryIso2)
        {
            return CountryIso2ToLangIso2[countryIso2.ToUpperInvariant()];
        }

        /// <summary>
        /// Mapping from country iso2 codes to language iso2 codes
        /// </summary>
        public static Dictionary<string, string> CountryIso2ToLangIso2 = new Dictionary<string, string>
                                                                         {
                                                                             {"AL", "EN"},
                                                                             {"AN", "ES"},
                                                                             {"AT", "DE"},
                                                                             {"BA", "BS"},
                                                                             {"BE", "NL"},
                                                                             {"BG", "BG"},
                                                                             {"BY", "EN"},
                                                                             {"CH", "DE"},
                                                                             {"CS", "SR"},
                                                                             {"CY", "EL"},
                                                                             {"CZ", "CS"},
                                                                             {"DE", "DE"},
                                                                             {"DK", "DA"},
                                                                             {"EE", "ET"},
                                                                             {"ES", "ES"},
                                                                             {"FI", "FI"},
                                                                             {"FR", "FR"},
                                                                             {"GB", "EN"},
                                                                             {"GR", "EL"},
                                                                             {"HR", "HR"},
                                                                             {"HU", "HU"},
                                                                             {"IE", "EN"},
                                                                             {"IS", "EN"},
                                                                             {"IT", "IT"},
                                                                             {"LI", "DE"},
                                                                             {"LT", "LT"},
                                                                             {"LU", "FR"},
                                                                             {"LV", "LV"},
                                                                             {"MC", "FR"},
                                                                             {"MD", "RO"},
                                                                             {"ME", "SR"},
                                                                             {"MK", "EN"},
                                                                             {"MT", "EN"},
                                                                             {"NL", "NL"},
                                                                             {"NO", "NO"},
                                                                             {"PL", "PL"},
                                                                             {"PT", "PT"},
                                                                             {"RO", "RO"},
                                                                             {"RS", "SR"},
                                                                             {"RU", "RU"},
                                                                             {"SE", "SV"},
                                                                             {"SI", "SL"},
                                                                             {"SK", "SK"},
                                                                             {"SM", "IT"},
                                                                             {"TR", "TR"},
                                                                             {"UA", "EN"}
                                                                         };

    	public Language GetLanguageForCulture(string culture)
    	{
			if (culture == null)
				throw new ArgumentNullException("culture");

			if (culture.StartsWith("de"))
				return GetGerman();

			throw new NotImplementedException();
    	}
    }
}
