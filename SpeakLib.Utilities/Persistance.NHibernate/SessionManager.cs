﻿using System;
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
        private readonly ISession _session;

        public SessionManager(ISession session)
        {
            _session = session;
		}

        public ISession Session
        {
            get { return _session; }
        }

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
