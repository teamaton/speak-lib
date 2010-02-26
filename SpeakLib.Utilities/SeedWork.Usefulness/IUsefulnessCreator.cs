using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace SpeakFriend.Utilities.Usefulness
{
	public interface IUsefulnessCreator
	{
		int Id { get; set; }
		/// <summary>
		/// Usually this should just be the Type of the class.
		/// </summary>
		string TypeName { get; }

//		ISet<IUsefulnessEntity> RatedEntities { get; set; }
	}
}
