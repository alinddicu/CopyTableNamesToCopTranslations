namespace CopyTableNamesToCopTranslations.AspNetCore.MissingDIExtensions
{
	using Ninject;
	using System;

	public static class BindingHelpers
	{
		public static void BindToMethod<T>(this IKernel config, Func<T> method) =>
			config.Bind<T>().ToMethod(c => method());
	}
}
