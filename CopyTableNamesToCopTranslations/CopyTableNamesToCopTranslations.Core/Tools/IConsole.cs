namespace CopyTableNamesToCopTranslations.Core.Tools
{
	using System;
	using System.Collections.Generic;

	public interface IConsole
	{
		void WriteLine(string line);

		ConsoleKeyInfo ReadKey();

		IEnumerable<string> Lines { get; }
	}
}
