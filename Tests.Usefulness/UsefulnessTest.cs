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
			_entity = new UsefulEntity();
			_usefulEntityService.Create(_entity);

			var usefulnessEntry = new UsefulnessEntry(_entity, 1);
			_usefulnessService.Create(usefulnessEntry);

			_nHibernateHelper.Flush();
			_nHibernateHelper.Clear();

			_entity = _usefulEntityService.GetById(_entity.Id);
		}

		[Test]
		public void Should_have_one_positive_vote()
		{
			Arrange_entity_with_one_positive_vote();
			
			Assert.That(_entity.Usefulness.Positive, EqualTo(1));
		}
	}
}
