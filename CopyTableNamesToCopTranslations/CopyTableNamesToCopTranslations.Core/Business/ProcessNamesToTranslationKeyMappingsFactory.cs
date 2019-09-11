namespace CopyTableNamesToCopTranslations.Core.Business
{
	using System.Collections.Generic;

	public class ProcessNamesToTranslationKeyMappingsFactory : IProcessNamesToTranslationKeyMappingsFactory
	{
		public Dictionary<string, string> Get()
		{
			return new Dictionary<string, string> {
				{ "SO - ERP-Customer material mapping", "SO - ERP/Customer material mapping" }, // OK
				{ "SO - Units of measure-Material mapping", "SO - Units of measure/Material mapping"}, // OK
				{ "SO - Ship to address-ship to number mapping", "SO - Ship to address/number mapping" }, // OK
				{ "SO - Sold to address-number mapping", "SO - Sold to address/number mapping" }, // OK
				{ "SO - Ship Method", "SO - ShipMethod" }, // OK
			};
		}
	}
}
