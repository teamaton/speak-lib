using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public class SettingService : IDataService<Setting>
    {
        private readonly ISettingRepository _repository;

        public SettingService(
            ISettingRepository repository)
        {
            _repository = repository;
        }

        public void Create(SettingList settings)
        {
            foreach (var setting in settings)
                Create(setting);
        }

        public void Create(Setting list)
        {
            list.DateCreated = list.DateModified = DateTime.Now;

            _repository.Create(list);
        }

        public void CreateOrUpdate(SettingList settings)
        {
            foreach (var setting in settings)
                CreateOrUpdate(setting);
        }

        public void Update(Setting setting)
        {
            setting.DateModified = DateTime.Now;
            _repository.Update(setting);
        }

        public void Update(SettingList settings)
        {
            foreach(var setting in settings)
                Update(setting);
        }

        public void CreateOrUpdate(Setting setting)
        {
            if (setting.DateCreated == DateTime.MinValue)
                setting.DateCreated = DateTime.Now;

            setting.DateModified = DateTime.Now;

            _repository.CreateOrUpdate(setting);
        }

        List<Setting> IDataService<Setting>.GetAll()
        {
            return GetAll();
        }

        public Setting GetById(int id)
        {
            throw new System.NotImplementedException();
        }

    	List<Setting> IDataService<Setting>.GetBy(ISearchDesc searchDesc)
        {
    		return _repository.GetBy((SettingSearchDesc) searchDesc);
        }

        public void Delete(Setting setting)
        {
            _repository.Delete(setting);
        }

        public void Delete(SettingList settingList)
        {
            foreach (var setting in settingList)
                Delete(setting);
        }

        public SettingList GetAll()
        {
            return _repository.GetAll();
        }

        private SettingList _allSettings;
        public SettingList GetAllCached()
        {
            if (_allSettings == null) _allSettings = GetAll();
            return _allSettings;
        }

        public SettingList GetBy(SettingSearchDesc searchDesc)
        {
            return _repository.GetBy(searchDesc);
        }

        /// <summary>
        /// Convenience method to get a single setting.
        /// </summary>
        /// <param name="settingSearchDesc"></param>
        /// <returns></returns>
        public T GetUnique<T>(SettingSearchDesc settingSearchDesc) where T : Setting, new() 
        {
            return _repository.GetUnique(settingSearchDesc) as T;
        }

        /// <summary>
        /// Load all persisted settings from the SB for the given settingType and settingTypeId.
        /// </summary>
        /// <param name="settingType"></param>
        /// <param name="settingTypeId"></param>
        /// <returns></returns>
        public SettingList GetBy(string settingType, int settingTypeId)
        {
            return GetBy(new SettingSearchDesc(settingType, settingTypeId));
        }

        public SettingList GetBy(string settingType)
        {
            var settingSearchDesc = new SettingSearchDesc();
            settingSearchDesc.Filter.SettingType.EqualTo(settingType);

            return GetBy(settingSearchDesc);
        }
    }
}
