namespace CopyTableNamesToCopTranslations.Program.AutomaticDI
{
	using CopyTableNamesToCopTranslations.Core.Business;
	using Ninject;
	using System.Reflection;

	public class Program
	{
		public static void Main(string[] args)
		{
			var kernel = new StandardKernel();
			kernel.Load(Assembly.GetExecutingAssembly());

			kernel.Get<CopyTableNamesToCopTranslations>().Execute();
		}
	}
}
