namespace CopyTableNamesToCopTranslations.Business
{
	public class CopLanguageFolderDefaultProvider : ICopLanguageFolderProvider
	{
		private const string CopLanguagegDir = @"D:\DEV\EOD\Delivery\Packages\O2C\SOP\SOP Generic\Pack B\Customer Order Processing\lg";

		public string Provide()
		{
			return CopLanguagegDir;
		}
	}
}
