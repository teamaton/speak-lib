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
			_nHibernateHelper.EmptyTable(typeof(UsefulTestEntity), typeof(UsefulnessEntry));
		}

		private UsefulTestEntity _testEntity;
		private const string _ipAddress = "127.0.0.1";

		private void Reload_entity()
		{
			_nHibernateHelper.Flush();
			_nHibernateHelper.Session.Evict(_testEntity);
			_testEntity = _usefulTestEntityService.GetById(_testEntity.Id);
		}

		private void Arrange_persisted_entity()
		{
			_testEntity = _usefulTestEntity; // gets new entity
			Assert.That(_testEntity.Usefulness, Is.Not.Null);
			Assert.That(_testEntity.Usefulness.Count, EqualTo(0));
			_usefulTestEntityService.Create(_testEntity);

			Reload_entity();
		}

		private void Arrange_entity_with_one_positive_vote()
		{
			Arrange_persisted_entity();

			var usefulnessEntry = new UsefulnessEntry(_testEntity, 1, _ipAddress, new UsefulnessCreatorAnonymous(_ipAddress, new TimeSpan(0,0,3)));
			_usefulnessService.Create(usefulnessEntry);

			Reload_entity();
		}

		private void Arrange_entity_with_one_positive_vote_by_anonymous()
		{
			Arrange_persisted_entity();

			var usefulnessEntry = new UsefulnessEntry(_testEntity, 1, "127.0.0.1", new UsefulnessCreatorAnonymous(_ipAddress, new TimeSpan(0, 0, 3)));
			_usefulnessService.Create(usefulnessEntry);

			Reload_entity();
		}

		[Test]
		public void Should_have_one_positive_vote()
		{
			Arrange_entity_with_one_positive_vote();

			Assert.That(_testEntity.Usefulness.Positive, EqualTo(1));
		}

		[Test]
		public void Should_have_anonymous_creator()
		{
			Arrange_entity_with_one_positive_vote_by_anonymous();

			Assert.That(_testEntity.Usefulness.Positive, EqualTo(1));

			var all = _usefulnessService.GetAll();

			Assert.That(all.Count, EqualTo(1));
			Assert.That(all[0].CreatorId, EqualTo(0));
			Assert.That(all[0].CreatorType, EqualTo(typeof(UsefulnessCreatorAnonymous).Name));
		}
	}
}
