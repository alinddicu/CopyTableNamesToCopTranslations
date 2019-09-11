namespace CopyTableNamesToCopTranslations.Tests
{
	using CopyTableNamesToCopTranslations.Core.Business;

	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NFluent;

	[TestClass]
	public class CopyTableNamesToCopTranslationsTests
	{
		// Basic nominal case for proof of concept for the blog article since the project "CopyTableNamesToCopTranslations.Core" is only a tool
		[TestMethod]
		[DeploymentItem(@"Resources\NominalCase")]
		public void NominalCase()
		{
			var testConsole = new ConsoleTestImpl();
			var copyTableNamesToCopTranslations = new CopyTableNamesToCopTranslations(
				new NominalCasePackBPathProvider(),
				new NominalCaseCopLanguageFolderProvider(),
				testConsole,
				new ProcessNamesToTranslationKeyMappingsFactory()
			);

			copyTableNamesToCopTranslations.Execute();

			Check.That(testConsole.Lines.Count()).IsStrictlyGreaterThan(0);
		}

		private class NominalCaseCopLanguageFolderProvider : ICopLanguageFolderProvider
		{
			public string Provide()
			{
				return @".\Pack B\Customer Order Processing\lg";
			}
		}

		private class NominalCasePackBPathProvider : IPackBPathProvider
		{
			public string Provide()
			{
				return @".\Pack B";
			}
		}
	}
}
