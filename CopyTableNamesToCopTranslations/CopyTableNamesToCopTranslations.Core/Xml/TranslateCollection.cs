namespace CopyTableNamesToCopTranslations.Core.Xml
{
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot]
	public class TranslateCollection
	{
		public TranslateCollection()
		{
			items = new List<TranslateItem>();
		}

		[XmlArray]
		public List<TranslateItem> items;

		[XmlAttribute]
		public bool frozen;
	}
}
