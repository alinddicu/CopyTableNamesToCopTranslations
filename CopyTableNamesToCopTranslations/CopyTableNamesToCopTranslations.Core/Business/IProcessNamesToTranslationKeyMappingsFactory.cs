namespace CopyTableNamesToCopTranslations.Core.Business
{
	using System.Collections.Generic;

	public interface IProcessNamesToTranslationKeyMappingsFactory
	{
		Dictionary<string, string> Get();
	}
}