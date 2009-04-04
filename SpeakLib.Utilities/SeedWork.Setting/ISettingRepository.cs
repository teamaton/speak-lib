namespace SpeakFriend.Utilities
{
    public interface ISettingRepository
    {
        void Create(Setting setting);
        void Update(Setting setting);
        void CreateOrUpdate(Setting setting);
        void Delete(Setting setting);

        SettingList GetAll();
        Setting GetById(int settingId);
        SettingList GetBy(SettingSearchDesc searchDesc);
        T GetUnique<T>(SettingSearchDesc searchDesc);
    }
}
