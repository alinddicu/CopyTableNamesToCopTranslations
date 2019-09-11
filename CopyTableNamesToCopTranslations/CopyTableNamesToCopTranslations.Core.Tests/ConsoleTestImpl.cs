namespace CopyTableNamesToCopTranslations.Core.Tests
{
	using CopyTableNamesToCopTranslations.Core.Tools;

	using System;
	using System.Collections.Generic;

	public class ConsoleTestImpl : IConsole
	{
		private readonly List<string> _lines = new List<string>();

		public void WriteLine(string line)
		{
			_lines.Add(line);
		}

		public IEnumerable<string> Lines => _lines;

		public ConsoleKeyInfo ReadKey()
		{
			return new ConsoleKeyInfo();
		}
	}
}