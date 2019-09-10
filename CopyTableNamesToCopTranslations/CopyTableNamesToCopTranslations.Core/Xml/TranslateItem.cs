namespace CopyTableNamesToCopTranslations.Xml
{
	using System.Xml;
	using System.Xml.Serialization;

	public class TranslateItem
	{
		public TranslateItem()
		{
		}

		[XmlElement]
		public string key;

		[XmlElement]
		public string val;
	}
}
