/// The work on PageStatePersister ist based on the work of "Allan Spartacus Mangune".
/// The original source can be found here: 
/// http://viewstatecontroller.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=11525#ReleaseFiles

namespace SpeakFriend.Utilities.Web
{
    public interface IViewStateStore
    {
        void Save(string id, object data);
        string Load(string id);
        void Delete(string id);
    }
}
