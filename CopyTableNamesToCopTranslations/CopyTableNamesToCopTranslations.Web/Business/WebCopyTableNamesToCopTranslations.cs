namespace CopyTableNamesToCopTranslations.AspNetCore.Business
{
	using CopyTableNamesToCopTranslations.Core.Business;
	using CopyTableNamesToCopTranslations.Core.Tools;
	using System.Collections.Generic;

	public class WebCopyTableNamesToCopTranslations: CopyTableNamesToCopTranslations
	{
		private readonly IConsole _console;

		public WebCopyTableNamesToCopTranslations(
				IPackBPathProvider packPathProvider,
				ICopLanguageFolderProvider copLanguageFolderProvider,
				IConsole console,
				IProcessNamesToTranslationKeyMappingsFactory mappingsFactory)
			: base(packPathProvider, copLanguageFolderProvider, console, mappingsFactory)
		{
			_console = console;
		}

		public IEnumerable<string> GetReport()
		{
			return _console.Lines;
		}
	}
}
