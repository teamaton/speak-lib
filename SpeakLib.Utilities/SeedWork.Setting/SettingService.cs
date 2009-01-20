using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace CampingInfo.Core
{
    
}

namespace SpeakFriend.Utilities
{
    public class SettingService
    {
        private bool _multiple = false;

        private readonly ISession _session;

        public SettingService(ISession session)
        {
            _session = session;
        }

        public void Create(SettingList settings)
        {
            _multiple = true;

            foreach (var setting in settings)
                Create(setting);

            _multiple = false;

            _session.Flush();
        }

        public void Create(Setting setting)
        {
            setting.DateCreated = setting.DateModified = DateTime.Now;

            _session.Save(setting);

            if (!_multiple)
                _session.Flush();
        }

        public void Update(SettingList settings)
        {
            _multiple = true;

            foreach (var setting in settings)
                CreateOrUpdate(setting);

            _multiple = false;

            _session.Flush();
        }

        public void Update(Setting setting)
        {
            setting.DateModified = DateTime.Now;

            _session.Update(setting);

            if (!_multiple)
                _session.Flush();
        }

        public void CreateOrUpdate(Setting setting)
        {
            if (setting.DateCreated == DateTime.MinValue)
                setting.DateCreated = DateTime.Now;

            setting.DateModified = DateTime.Now;

            _session.SaveOrUpdate(setting);

            if (!_multiple)
                _session.Flush();
        }

        public void Save(Setting setting)
        {
            if (!setting.IsDefault())
            {

                // DateCreated has not been set before; this setting has not been saved before.
                if (setting.DateCreated == DateTime.MinValue)
                    setting.DateCreated = DateTime.Now;

                setting.DateModified = DateTime.Now;

                /*
                 * This is somewhat special: setting may not be the object that was associated
                 * with the current session by NHibernate. That's why we store it's values in
                 * the persisted object.
                 * See Get<T>() for more detail.
                 */ 
                _session.SaveOrUpdateCopy(setting);
                _session.Flush();
            }
            else
            {
                /*
                 * This is somewhat special: setting may not be the object that was associated
                 * with the current session by NHibernate. That's why we first get the persisted
                 * object and then delete it.
                 * See Get<T>() for more detail.
                 */
                var toDelete = _session.Get<Setting>(setting.Id);
                if (toDelete != null)
                    Delete(toDelete);
            }
        }

        public void Save(SettingList settings)
        {
            foreach (var setting in settings)
                Save(setting);
        }

        public void Delete(Setting setting)
        {
            _session.Delete(setting);
            _session.Flush();
        }

        public void Delete(SettingList settingList)
        {
            foreach (var setting in settingList)
                Delete(setting);
        }

        public SettingList GetAll()
        {
            return new SettingList(_session.CreateCriteria(typeof(Setting)).List<Setting>());
        }

        public T GetByKey<T>(string key) where T : Setting, new()
        {
            return _session.CreateCriteria(typeof(Setting))
                .Add(Restrictions.Eq("Key", key))
                .UniqueResult<T>();
        }

        /// <summary>
        /// Fills the values of the settings with the DB values where they differ from the
        /// default. Gets only values for a specific instance of SettingList (with specific
        /// SettingType and SettingTypeID set).
        /// </summary>
        /// <typeparam name="T">Specific class that inherits SettingList.</typeparam>
        /// <param name="settings">The list of settings.</param>
        /// <returns>The setting list filled with values from the DB.</returns>
        public T Load<T>(T settings) where T : EntitySettings
        {
            if (settings.Count <= 0)
                return settings;

            var settingsFromDb = _session.CreateCriteria(typeof(Setting))
                .Add(Restrictions.Eq("SettingType", settings.SettingType))
                .Add(Restrictions.Eq("SettingTypeId", settings.SettingTypeId))         
                .List<Setting>();

            foreach (var setting in settingsFromDb)
            {
                var tempSetting = setting;
                var foundSetting = settings.Find(inList => inList.Key == tempSetting.Key);

                /*
                 * This is special: copy the values of the persisted object to a different instance.
                 * If you tried to save that instance with Save/[Or]/Update/Delete you would get a
                 * NonUniqueObjectException because NHibernate expects the object it created (here:
                 * setting) when you want to save it.
                 * That's why we use SaveOrUpdateCopy when we want to save our custom object with
                 * the same identifier.
                 */ 
                foundSetting.Id = setting.Id;
                foundSetting.ValueStr = setting.ValueStr;
                foundSetting.DateCreated = setting.DateCreated;
                foundSetting.DateModified = setting.DateModified;
            }

            return settings;
        }

        public SettingList GetBy(SettingType type)
        {
            var settingsFromDb = _session.CreateCriteria(typeof(Setting))
                .Add(Restrictions.Eq("SettingType", type))
                .List<Setting>();

            return new SettingList(settingsFromDb);
        }

        public SettingList GetBy(SettingType type, int typeId)
        {
            var settingsFromDb = _session.CreateCriteria(typeof(Setting))
                .Add(Restrictions.Eq("SettingType", type))
                .Add(Restrictions.Eq("SettingTypeId", typeId))
                .List<Setting>();

            return new SettingList(settingsFromDb);
        }

    }
}
