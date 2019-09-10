namespace Program.AutomaticDI
{
	using CopyTableNamesToCopTranslations.Business;
	using CopyTableNamesToCopTranslations.Tools;
	using CopyTableNamesToCopTranslations.Program;

	using Ninject;
	using Ninject.Modules;

	public class DependencyInjectionInitializer : NinjectModule
	{
		public override void Load()
		{
			Bind<IConsole>().To<StandardConsole>();
			Bind<IProcessNamesToTranslationKeyMappingsFactory>().To<ProcessNamesToTranslationKeyMappingsFactory>();
			Bind<IPackBPathProvider>().To<PackBPathDefaultProvider>();
			Bind<ICopLanguageFolderProvider>().To<CopLanguageFolderDefaultProvider>();
		}
	}
}
