using FluentNHibernate.Mapping;
using SpeakFriend.Utilities;

namespace Teamaton.Lib
{
	public class LanguageMapping : ClassMap<Language>
	{
		public LanguageMapping()
		{
			Id(x => x.Iso2).Column("Id").CustomType(typeof (string)).Length(2);
			Map(x => x.Name);
			Map(x => x.DateCreated);
			Table("Languages");
		}
	}
}