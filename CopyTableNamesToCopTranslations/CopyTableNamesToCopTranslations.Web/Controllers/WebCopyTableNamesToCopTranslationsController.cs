namespace CopyTableNamesToCopTranslations.AspNetCore.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.AspNetCore.Mvc;
	using CopyTableNamesToCopTranslations.AspNetCore.Business;

	[Route("api/[controller]")]
	[ApiController]
	public class WebCopyTableNamesToCopTranslationsController : ControllerBase
	{
		private readonly WebCopyTableNamesToCopTranslations _copyTableNamesToCopTranslations;

		public WebCopyTableNamesToCopTranslationsController(WebCopyTableNamesToCopTranslations copyTableNamesToCopTranslations) : base()
		{
			_copyTableNamesToCopTranslations = copyTableNamesToCopTranslations;
		}

		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			_copyTableNamesToCopTranslations.Execute();
			return _copyTableNamesToCopTranslations.GetReport().ToArray();
		}
	}
}