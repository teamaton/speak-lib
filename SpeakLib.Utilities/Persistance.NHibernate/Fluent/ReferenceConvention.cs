using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SpeakFriend.Utilities
{
	public class ReferenceConvention : IReferenceConvention
	{
		public void Apply(IManyToOneInstance instance)
		{
			instance.Column(instance.Property.Name + "Id");
		}
	}

	//public class PrimaryKeyNameConvention : IIdConvention
	//{
	//    public void Apply(IIdentityInstance instance)
	//    {
	//        instance.Column(instance.EntityType.Name + "Id");
	//    }
	//}

	public class ForeignKeyNameConvention : IHasManyConvention
	{
		public void Apply(IOneToManyCollectionInstance instance)
		{
			instance.Key.Column(instance.EntityType.Name + "Id");
		}
	}

}