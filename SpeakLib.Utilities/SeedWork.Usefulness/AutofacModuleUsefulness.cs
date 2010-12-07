using Autofac.Builder;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Usefulness
{
	public class AutofacModuleUsefulness : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<UsefulnessService>().ContainerScoped();

			builder.RegisterTypesMatching(t => typeof (IUsefulnessEntity).IsAssignableFrom(t)).FactoryScoped();
		}
	}
}