namespace CopyTableNamesToCopTranslations.Business
{
	using System.Collections.Generic;

	public interface IProcessNamesToTranslationKeyMappingsFactory
	{
		Dictionary<string, string> Get();
	}
}