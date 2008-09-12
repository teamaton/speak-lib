using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SpeakFriend.TrueOrFalse
{
    /// <summary>
    /// Hilfsklasse, die bestimmte NHibernate-Operationen direkt ausführt,
    /// die nur für Testzwecke gebraucht werden und daher nicht in der
    /// Anwendungsschicht enthalten sind.
    /// </summary>
    public class NHibernateHelper
    {
        internal ISession _session;

        public NHibernateHelper(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Löscht den Inhalt einer Tabelle komplett und versetzt
        /// den Index Counter in seinen Initialzustand.
        /// </summary>
        /// <param name="tableName">Der Name der zu leerenden Tabelle.</param>
        public void TruncateTable(string tableName)
        {
            _session
                .CreateSQLQuery(String.Format("TRUNCATE TABLE {0}", tableName))
                .ExecuteUpdate();

            _session.Flush();
        }
        
        public void TruncateTable_Question()
        {
            TruncateTable("Question");
        }

        public void TruncateAll()
        {
            TruncateTable_Question();
        }
    }
}

