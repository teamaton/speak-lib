using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Überwacht den Lifecycle einer NHibernate Session, damit
    /// Daten vor dem Schließen gespeichert werden.
    /// </summary>
    public class SessionManager : ISessionManager
    {
//		public static int Counter = 0;
        private readonly ISession _session;

        public SessionManager(ISession session)
        {
            _session = session;
//			Console.WriteLine("New SessionManager: {0} (SesMgr: {1})", Session.GetHashCode(), GetHashCode());
//        	Counter++;
		}

        public ISession Session
        {
            get { return _session; }
        }

        public void Dispose()
        {
            if (Session.IsOpen)
            {
//				Console.WriteLine("Closing NH session: {0} (SesMgr: {1})", Session.GetHashCode(), GetHashCode());
                Session.Flush();
                Session.Close();
            }

            Session.Dispose();
        }
    }
}
