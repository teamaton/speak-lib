using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;

namespace SpeakFriend.Utilities.Persistance.NHibernate.Fluent
{
	public class TableNameConvention : ManyToManyTableNameConvention
	{
		protected override string GetBiDirectionalTableName(IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
		{
			return collection.EntityType.Name + "_" + otherSide.EntityType.Name;
		}

		protected override string GetUniDirectionalTableName(IManyToManyCollectionInspector collection)
		{
			return collection.EntityType.Name + "_" + collection.ChildType.Name;
		}
	}
}
