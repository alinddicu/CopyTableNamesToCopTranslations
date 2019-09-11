namespace CopyTableNamesToCopTranslations.Tests
{
	using CopyTableNamesToCopTranslations.Core.Business;

	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using NFluent;
	using System.IO;

	[TestClass]
	public class CopyTableNamesToCopTranslationsTests
	{
		// Basic nominal case for proof of concept for the blog article since the project "CopyTableNamesToCopTranslations.Core" is only a tool
		[TestMethod]
		[DeploymentItem(@"Resources\NominalCase")]
		public void NominalCase()
		{
			var copLanguageFolder = @".\Pack B\Customer Order Processing\lg";

			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_de.xml")).IsEqualTo(1);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_en.xml")).IsEqualTo(1);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_fr.xml")).IsEqualTo(1);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_CT.xml")).IsEqualTo(2);

			var testConsole = new ConsoleTestImpl();
			var copyTableNamesToCopTranslations = new CopyTableNamesToCopTranslations(
				new NominalCasePackBPathProvider(),
				new NominalCaseCopLanguageFolderProvider(copLanguageFolder),
				testConsole,
				new ProcessNamesToTranslationKeyMappingsFactory()
			);

			copyTableNamesToCopTranslations.Execute();

			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_fr.xml")).IsEqualTo(3);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_en.xml")).IsEqualTo(3);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_fr.xml")).IsEqualTo(3);
			Check.That(GetTranslatedItemsCount(copLanguageFolder, "process_CT.xml")).IsEqualTo(2);

			Check.That(testConsole.Lines.Count()).IsStrictlyGreaterThan(0);
		}

		private int GetTranslatedItemsCount(string copLanguageFolder, string copLanguageFile)
		{
			var fullPath = Path.Combine(copLanguageFolder, copLanguageFile);
			return CopyTableNamesToCopTranslations.GetAllCopTranslations(fullPath).items.Count;
		}

		private class NominalCaseCopLanguageFolderProvider : ICopLanguageFolderProvider
		{
			private readonly string _copLanguageFolder;

			public NominalCaseCopLanguageFolderProvider(string copLanguageFolder)
			{
				_copLanguageFolder = copLanguageFolder;
			}

			public string Provide()
			{
				return _copLanguageFolder;
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
