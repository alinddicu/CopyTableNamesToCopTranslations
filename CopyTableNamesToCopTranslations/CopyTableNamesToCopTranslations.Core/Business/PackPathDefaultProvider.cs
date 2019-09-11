namespace CopyTableNamesToCopTranslations.Core.Business
{
	public class PackBPathDefaultProvider : IPackBPathProvider
	{
		public const string PackBPath = @"D:\DEV\EOD\Delivery\Packages\O2C\SOP\SOP Generic\Pack B";

		public string Provide()
		{
			return PackBPath;
		}
	}
}
