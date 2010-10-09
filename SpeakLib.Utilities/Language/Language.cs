using System;
using SpeakFriend.Utilities;

namespace Teamaton.Lib
{
	[Serializable]
	public class Language
	{
		private string _iso2;

		/// <summary>
		/// Is always UPPERCASE.
		/// </summary>
		public virtual string Iso2
		{
			get { return _iso2.ToUpperInvariant(); }
			private set { _iso2 = value; }
		}

		public virtual string Name { get; private set; }
		public virtual DateTime DateCreated { get; private set; }

		public Language()
		{
		}

		public Language(string iso2, string name)
		{
			Iso2 = iso2;
			Name = name;
			DateCreated = DateTime.Now;
		}

		public override string ToString()
		{
			return String.Format("Iso2:{0} Name:{1}", Iso2, Name);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (GetType() != obj.GetType())
				return false;

			var language = (Language) obj;

			if (language.Iso2 == Iso2)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return Iso2.GetHashCode();
		}

		public static bool operator ==(Language x, Language y)
		{
			var xnull = ReferenceEquals(x, null);
			var ynull = ReferenceEquals(y, null);

			if (xnull && ynull) return true;
			if (xnull || ynull) return false;

			return x.Equals(y);
		}

		public static bool operator !=(Language x, Language y)
		{
			return !(x == y);
		}
	}
}