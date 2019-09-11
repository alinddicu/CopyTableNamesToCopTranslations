namespace CopyTableNamesToCopTranslations.Program
{
	using CopyTableNamesToCopTranslations.Core.Tools;
	using System;
	using System.Collections.Generic;

	public class StandardConsole : IConsole
	{
		public IEnumerable<string> Lines => new List<string>();

		public ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey();
		}

		public void WriteLine(string line)
		{
			Console.WriteLine(line);
		}
	}
}
