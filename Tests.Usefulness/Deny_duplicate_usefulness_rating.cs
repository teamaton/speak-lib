using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	[TestFixture]
	public class Deny_duplicate_usefulness_rating : BaseTest
	{
		private UsefulTestEntity _testEntity;
		private UsefulTestCreator _creator;
		private const string _ipAddress = "127.0.0.1";

		private void Arrange_entity_with_usefulnessentry_by_visitor()
		{
			_testEntity = _usefulTestEntity;
			_nHibernateHelper.Session.Save(_usefulTestEntity);
			_creator = new UsefulTestCreator();
			_usefulTestCreatorService.Create(_creator);

			var entry = new UsefulnessEntry(_testEntity, 1, _ipAddress, _creator);
			_usefulnessService.Create(entry);
		}

		private void Should_not_see_button_to_submit_usefulnessrating()
		{
			_creator = _usefulTestCreatorService.GetById(_creator.Id);
			var entries = _usefulnessService.GetUsefulnessEntitiesByCreator(_creator);
			var usefulEntityIds = entries.GetEntityIdsByEntityType(_testEntity);
			Assert.That(usefulEntityIds, Contains(_testEntity.Id));
		}

		[Test]
		public void When_visitor_reads_comment_already_rated_by_him()
		{
			Arrange_entity_with_usefulnessentry_by_visitor();

			Should_not_see_button_to_submit_usefulnessrating();
		}
	}
}
