using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using interceptors.Infrastructure;
using interceptors.Infrastructure.Interceptors.Setup;
using interceptors.Services;
using interceptors.Infrastructure.Interceptors;

namespace interceptors
{
	/// <summary>
	/// �������
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// ��������� ����������
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// �������
		/// </summary>		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// �������� DI
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			// ��������� �������
			services.AddTransient<IWeatherForecastService, WeatherForecastService>();

			// ��������� �����������
			services.AddControllers();

			// ��������� �������������
			services.AddAdvicesOf<IApplicationService>();			
		}

		/// <summary>
		/// ��������� ����������
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
