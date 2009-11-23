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
		public override void SetUp()
		{
			base.SetUp();
			_nHibernateHelper.EmptyTable(typeof(UsefulEntity), typeof(UsefulnessEntry));
		}

		private UsefulEntity _entity;

		private void Reload_entity()
		{
			_nHibernateHelper.Flush();
			_nHibernateHelper.Session.Evict(_entity);
			_entity = _usefulEntityService.GetById(_entity.Id);
		}

		private void Arrange_persisted_entity()
		{
			_entity = _usefulEntity; // gets new entity
			Assert.That(_entity.Usefulness, Is.Not.Null);
			Assert.That(_entity.Usefulness.Count, EqualTo(0));
			_usefulEntityService.Create(_entity);

			Reload_entity();
		}

		private void Arrange_entity_with_one_positive_vote()
		{
			Arrange_persisted_entity();

			var usefulnessEntry = new UsefulnessEntry(_entity, 1);
			_usefulnessService.Create(usefulnessEntry);

			Reload_entity();
		}

		private void Arrange_entity_with_one_positive_vote_by_anonymous()
		{
			Arrange_persisted_entity();

			var usefulnessEntry = new UsefulnessEntry(_entity, 1, new UsefulnessCreatorAnonymous());
			_usefulnessService.Create(usefulnessEntry);

			Reload_entity();
		}

		[Test]
		public void Should_have_one_positive_vote()
		{
			Arrange_entity_with_one_positive_vote();

			Assert.That(_entity.Usefulness.Positive, EqualTo(1));
		}

		[Test]
		public void Should_have_anonymous_creator()
		{
			Arrange_entity_with_one_positive_vote_by_anonymous();

			Assert.That(_entity.Usefulness.Positive, EqualTo(1));

			var all = _usefulnessService.GetAll();

			Assert.That(all.Count, EqualTo(1));
			Assert.That(all[0].CreatorId, EqualTo(0));
			Assert.That(all[0].CreatorType, EqualTo(typeof(UsefulnessCreatorAnonymous).Name));
		}
	}
}
