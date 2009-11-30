using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	[TestFixture]
	public class Deny_duplicate_usefulness_rating : BaseTest
	{
		#region Members

		private IUsefulnessEntity _testEntity;
		private IUsefulnessCreator _creator;
		private const string _ipAddress = "127.0.0.1";

		#endregion

		#region Arrange

		private void Arrange_entity()
		{
			_testEntity = _usefulTestEntity;
			_nHibernateHelper.Session.Save(_usefulTestEntity);
		}

		private void Arrange_user_as_creator()
		{
			_creator = new UsefulTestCreator();
			_nHibernateHelper.Session.Save(_creator);
			_nHibernateHelper.Flush();
			_nHibernateHelper.Session.Evict(_creator);
			_creator = _usefulTestCreatorService.GetById(_creator.Id);
		}

		private void Arrange_visitor_as_creator()
		{
			_creator = new UsefulnessCreatorAnonymous (_ipAddress, new TimeSpan(0,0,3));
		}

		private void Arrange_usefulnessentry()
		{
			var entry = new UsefulnessEntry(_testEntity, 1, _ipAddress, _creator);
			_usefulnessService.Create(entry);
		}

		private void Arrange_entity_with_usefulnessentry_by_user()
		{
			Arrange_entity();
			Arrange_user_as_creator();
			Arrange_usefulnessentry();
		}

		private void Arrange_entity_with_usefulnessentry_by_visitor()
		{
			Arrange_entity();
			Arrange_visitor_as_creator();
			Arrange_usefulnessentry();
		}

		#endregion

		#region Expectations

		private void Should_not_see_button_to_submit_usefulnessrating()
		{
			var entries = _usefulnessService.GetUsefulnessEntitiesByCreator(_creator);
			var usefulEntityIds = entries.GetEntityIdsByEntityType(_testEntity);
			Assert.That(usefulEntityIds, Contains(_testEntity.Id));
		}

		private void Should_see_button_to_submit_usefulnessrating()
		{
			var entries = _usefulnessService.GetUsefulnessEntitiesByCreator(_creator);
			var usefulEntityIds = entries.GetEntityIdsByEntityType(_testEntity);
			Assert.That(usefulEntityIds, Not.Contains(_testEntity.Id));
		}

		#endregion

		[Test]
		public void When_visitor_reads_comment_already_rated_from_same_ip()
		{
			Arrange_entity_with_usefulnessentry_by_visitor();

			Should_not_see_button_to_submit_usefulnessrating();
		}

		[Test]
		public void When_user_reads_comment_already_rated_by_him()
		{
			Arrange_entity_with_usefulnessentry_by_user();

			Should_not_see_button_to_submit_usefulnessrating();
		}

		[Test]
		public void When_visitor_reads_comment_rated_from_same_ip_long_ago()
		{
			Arrange_entity_with_usefulnessentry_by_visitor();
            Arrange_time_has_passed();

			Should_see_button_to_submit_usefulnessrating();
		}

		private void Arrange_time_has_passed()
		{
			var timeSpanThreshold = new TimeSpan(0, 0, 3);
			var sleepMs = timeSpanThreshold.Seconds*1000 + 1000;
			Console.Write("[{0}] Falling asleep for {1} ms ...", DateTime.Now.ToLongTimeString(), sleepMs);
			Thread.Sleep(sleepMs);
			Console.WriteLine("... awake! [{0}]", DateTime.Now.ToLongTimeString());

			((UsefulnessCreatorAnonymous) _creator).BlockingPeriod = timeSpanThreshold;
		}
	}
}
