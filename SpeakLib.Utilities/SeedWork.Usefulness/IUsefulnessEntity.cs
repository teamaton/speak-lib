using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public interface IUsefulnessEntity
	{
		int Id { get; set; }
		UsefulnessValue Usefulness { get; }
	}
}
