using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SpeakFriend.TrueOrFalse
{
    /// <summary>
    /// Überwacht den Lifecycle einer Session, damit
    /// Daten vor dem Schließen gespeichert werden.
    /// </summary>
    public class SessionManager : IDisposable
    {
        public SessionManager(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; private set; }

        public void Dispose()
        {
            if (Session.IsOpen)
            {
                Session.Flush();
                Session.Close();
            }

            Session.Dispose();
        }
    }
}
