using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using advicetest.Infrastructure;
using advicetest.Infrastructure.Interceptors.Setup;
using advicetest.Services;

namespace advicetest
{
	/// <summary>
	/// Стартап
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Настройки приложения
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Стартап
		/// </summary>		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Настрока DI
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			// Добавляем сервисы
			services.AddTransient<IWeatherForecastService, WeatherForecastService>();

			// Добавляем контроллеры
			services.AddControllers();

			// Настройка перехватчиков
			services.AddAdvicesOf<IApplicationService>();			
		}

		/// <summary>
		/// Настройка приложения
		/// </summary>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
