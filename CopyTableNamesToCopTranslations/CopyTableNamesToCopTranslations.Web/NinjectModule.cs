namespace CopyTableNamesToCopTranslations.AspNetCore
{
	using CopyTableNamesToCopTranslations.AspNetCore.Business;
	using CopyTableNamesToCopTranslations.Core.Business;
	using CopyTableNamesToCopTranslations.Core.Tools;
	using Ninject;

	public class NinjectModule
	{
		public void AddBindings(IKernel kernel)
		{
			kernel.Bind<IConsole>().To<WebConsole>();
			kernel.Bind<IProcessNamesToTranslationKeyMappingsFactory>().To<ProcessNamesToTranslationKeyMappingsFactory>();
			kernel.Bind<IPackBPathProvider>().To<PackBPathDefaultProvider>();
			kernel.Bind<ICopLanguageFolderProvider>().To<CopLanguageFolderDefaultProvider>();
		}
	}
}
