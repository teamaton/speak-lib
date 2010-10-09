using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace SpeakFriend.Utilities
{
	public class LanguageMapping : ClassMap<Language>
	{
		public LanguageMapping()
		{
			Id(x => x.Iso2).Column("Id").CustomType(typeof(string)).Length(2);
			Map(x => x.Name);
			Map(x => x.DateCreated);
			Table("Languages");
		}
	}
}
