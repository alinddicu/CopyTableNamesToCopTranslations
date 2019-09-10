namespace CopyTableNamesToCopTranslations.Program
{
	using CopyTableNamesToCopTranslations.Tools;
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
