using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// This convention tells Fluent NHibernate to map all Enums to ints instead of strings
	/// (string mapping is the default in FluentNH).
	/// </summary>
	/// <remarks>
	/// http://stackoverflow.com/questions/439003/how-do-you-map-an-enum-as-an-int-value-with-fluent-nhibernate
	/// </remarks>
	public class EnumConvention : IUserTypeConvention
	{
		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
		{
			criteria.Expect(x => x.Property.PropertyType.IsEnum);
		}

		public void Apply(IPropertyInstance target)
		{
			target.CustomType(target.Property.PropertyType);
		}
	}
}