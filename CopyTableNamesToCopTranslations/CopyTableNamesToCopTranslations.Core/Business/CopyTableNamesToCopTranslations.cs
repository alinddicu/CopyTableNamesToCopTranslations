namespace CopyTableNamesToCopTranslations.Core.Business
{
	using Tools;
	using Xml;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Xml;
	using System.Xml.Serialization;

	public class CopyTableNamesToCopTranslations
	{
		private readonly string _packBPath;
		private readonly string _copLanguageFolder;
		private readonly IConsole _console;
		private readonly Dictionary<string, string> _processNamesToTranslationKeyMappings;

		public CopyTableNamesToCopTranslations(
			IPackBPathProvider packPathProvider,
			ICopLanguageFolderProvider copLanguageFolderProvider,
			IConsole console,
			IProcessNamesToTranslationKeyMappingsFactory mappingsFactory)
		{
			_packBPath = packPathProvider.Provide();
			_copLanguageFolder = copLanguageFolderProvider.Provide();
			_console = console;
			_processNamesToTranslationKeyMappings = mappingsFactory.Get();
		}

		public void Execute()
		{
			var newTranslationsPerLanguage = new Dictionary<string, int>();
			var tableDirectories = Directory.EnumerateDirectories(_packBPath, "_SO*").ToList();			
			var copLanguages = GetCopLanguages().ToList();

			foreach (var copLanguage in copLanguages)
			{
				var gotoNextLanguage = false;
				var newTableTranslatedItems = new List<TranslateItem>();
				foreach (var directory in tableDirectories)
				{
					var tableDirectoryName = Path.GetFileName(directory);
					var tableProcessName = tableDirectoryName.TrimStart('_');
					var tableLanguageFile = $@"{_packBPath}\{tableDirectoryName}\lg\process_{copLanguage}.xml";
					if (!File.Exists(tableLanguageFile))
					{
						gotoNextLanguage = true;
						break;
					}

					var tableTranslations = GetCtTranslations(tableLanguageFile);
					AddCtTranslations(copLanguage, newTableTranslatedItems, tableProcessName, tableTranslations);
				}

				if (gotoNextLanguage)
				{
					continue;
				}

				AppendNewCtTranslationsToCopTranslations(newTableTranslatedItems, copLanguage);

				newTranslationsPerLanguage[copLanguage] = newTableTranslatedItems.Count;
			}

			WriteReport(newTranslationsPerLanguage, copLanguages.Count, tableDirectories);

			_console.ReadKey();
		}

		private IEnumerable<string> GetCopLanguages()
		{
			return Directory.EnumerateFiles(_copLanguageFolder)
							.Select(d => Path.GetFileName(d))
							.Select(f => f.Replace("process_", ""))
							.Select(f => f.Replace(".xml", ""))
							.Except(new[] { "CT" })
							.Where(l => !string.IsNullOrEmpty(l));
		}

		private void AddCtTranslations(string copLanguage, List<TranslateItem> newTableTranslatedItems, string tableProcessName, TranslateCollection tableTranslations)
		{
			var tableProcessNameKey = tableProcessName;
			tableProcessNameKey = _processNamesToTranslationKeyMappings.TryGetValue(tableProcessName, out tableProcessNameKey) ? tableProcessNameKey : tableProcessName;
			var newTableTranslatedItem = tableTranslations.items.SingleOrDefault(i => i.key == tableProcessNameKey);
			if (newTableTranslatedItem != null)
			{
				// transform key as table technical name
				newTableTranslatedItem.key = newTableTranslatedItem.key + "__";
				newTableTranslatedItems.Add(newTableTranslatedItem);
			}
			else
			{
				Report($"Error: no translation found for tableProcessNameKey: '{tableProcessNameKey}' for copLanguage: '{copLanguage}'");
			}
		}

		private void AppendNewCtTranslationsToCopTranslations(IEnumerable<TranslateItem> newTableTranslatedItems, string copLanguage)
		{
			var copLanguageFile = $@"{_copLanguageFolder}\process_{copLanguage}.xml";
			var allCopTranslations = GetAllCopTranslations(copLanguageFile);
			var newNonExistingTableTranslatedItems = newTableTranslatedItems.Where(ti => allCopTranslations.items.All(ti2 => ti.key != ti2.key));
			allCopTranslations.items.AddRange(newNonExistingTableTranslatedItems);

			string allCopTranslationsXml;
			using (var stringWritter = new Utf8StringWriter())
			using (var xmlWriter = XmlWriter.Create(stringWritter, new XmlWriterSettings { IndentChars = "\t", Indent = true }))
			{
				var ns = new XmlSerializerNamespaces();
				ns.Add("", "");
				new XmlSerializer(typeof(TranslateCollection)).Serialize(xmlWriter, allCopTranslations, ns);
				allCopTranslationsXml = stringWritter.ToString();
			}

			File.WriteAllText(copLanguageFile, allCopTranslationsXml, Encoding.UTF8);
		}

		public static TranslateCollection GetAllCopTranslations(string copLanguageFile)
		{
			TranslateCollection allCopTranslations;
			using (var reader = new FileStream(copLanguageFile, FileMode.Open))
			{
				// Call the Deserialize method to restore the object's state.
				allCopTranslations = (TranslateCollection)new XmlSerializer(typeof(TranslateCollection)).Deserialize(reader);
			}

			return allCopTranslations;
		}

		private static TranslateCollection GetCtTranslations(string tableLanguageFile)
		{
			TranslateCollection tableTranslations;
			using (var reader = new FileStream(tableLanguageFile, FileMode.Open))
			{
				tableTranslations = (TranslateCollection)new XmlSerializer(typeof(TranslateCollection)).Deserialize(reader);
			}

			return tableTranslations;
		}

		private void WriteReport(Dictionary<string, int> newTranslationsPerLanguage, int copLanguagesCount, IEnumerable<string> tabledirectories)
		{
			var tableProcessNames = tabledirectories.Select(d => Path.GetFileName(d)).Select(d => d.TrimStart('_')).ToList();

			Report();
			Report($"tableProcessNames count: {tableProcessNames.Count}");
			Report();
			Report($"copLanguages count: {copLanguagesCount}");
			Report($"table languages count: {newTranslationsPerLanguage.Keys.Count}");

			Report();
			foreach (var language in newTranslationsPerLanguage.Keys)
			{
				Report($"{language} count: {newTranslationsPerLanguage[language]}");
			}
		}

		private void Report(string reportLine = null)
		{
			_console.WriteLine(reportLine ?? string.Empty);
		}
	}
}
