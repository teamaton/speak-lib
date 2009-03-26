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
            list.Created = list.Modified = DateTime.Now;

            _repository.Create(list);
        }

        public void Update(SettingList settings)
        {
            foreach (var setting in settings)
                CreateOrUpdate(setting);
        }

        public void Update(Setting list)
        {
            list.Modified = DateTime.Now;

            _repository.Update(list);
        }

        public void CreateOrUpdate(Setting setting)
        {
            if (setting.Created == DateTime.MinValue)
                setting.Created = DateTime.Now;

            setting.Modified = DateTime.Now;

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

        public SettingList GetBy(SettingSearchDesc searchDesc)
        {
            return _repository.GetBy(searchDesc);
        }

        public void Save(Setting setting)
        {
            throw new NotImplementedException();
        }

        public void Save(SettingList settings)
        {
            foreach (var setting in settings)
                Save(setting);
        }

        

    }
}
