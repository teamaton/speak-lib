using System;
using NUnit.Framework;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestFixture]
	public class UsefulnessTest : BaseTest
	{
		private UsefulEntity _entity;

		private void Arrange_entity_with_one_positive_vote()
		{
			_entity = _usefulEntity; // gets new entity
			_usefulEntityService.Create(_entity);

			var usefulnessEntry = new UsefulnessEntry(_entity, 1);
			_usefulnessService.Create(usefulnessEntry);

			_nHibernateHelper.Flush();
			_nHibernateHelper.Clear();
			var entityId = _entity.Id;
			_entity = null;

			_entity = _usefulEntityService.GetById(entityId);
		}

		private void Arrange_usefulness_loaded_in_entity()
		{
			var pos = _entity.Usefulness.Positive; // initialize values
		}

		[Test]
		public void Should_have_one_positive_vote()
		{
			Arrange_entity_with_one_positive_vote();

			Arrange_usefulness_loaded_in_entity();
			
			Assert.That(_entity.Usefulness.Positive, EqualTo(1));
		}
	}
}
