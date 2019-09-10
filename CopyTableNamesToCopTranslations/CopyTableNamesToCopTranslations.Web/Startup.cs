namespace CopyTableNamesToCopTranslations.AspNetCore
{
	using System;
	using System.Threading;
	using CopyTableNamesToCopTranslations.AspNetCore.MissingDIExtensions;
	using global::MissingDIExtensions;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Ninject;
	using Ninject.Activation;
	using Ninject.Infrastructure.Disposal;

	public class Startup
	{
		private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
		private IKernel Kernel { get; set; }

		private object Resolve(Type type) => Kernel.Get(type);
		private object RequestScope(IContext context) => scopeProvider.Value;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
			services.AddCustomControllerActivation(Resolve);
			services.AddCustomViewComponentActivation(Resolve);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			Kernel = RegisterApplicationComponents(app, loggerFactory);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}

		private IKernel RegisterApplicationComponents(
			IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			// IKernelConfiguration config = new KernelConfiguration();
			Kernel = new StandardKernel();

			// Register application services
			foreach (var ctrlType in app.GetControllerTypes())
			{
				Kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
			}

			new NinjectModule().AddBindings(Kernel);

			// Cross-wire required framework services
			Kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);
			Kernel.Bind<ILoggerFactory>().ToConstant(loggerFactory);

			return Kernel;
		}

		private sealed class Scope : DisposableObject { }
	}
}
