namespace CopyTableNamesToCopTranslations.Program
{
	using CopyTableNamesToCopTranslations.Core.Business;

	public static class Program
	{

		public static void Main(string[] args)
		{
			new CopyTableNamesToCopTranslations(
				new PackBPathDefaultProvider(),
				new CopLanguageFolderDefaultProvider(),
				new StandardConsole(),
				new ProcessNamesToTranslationKeyMappingsFactory())
			.Execute();
		}
	}
}
