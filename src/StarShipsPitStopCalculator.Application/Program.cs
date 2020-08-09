using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Application
{
	public class Program
	{
		private static StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings c_appSettings;
		private static ServiceProvider c_serviceProvider;


		static async Task Main()
		{
			try
			{
				BuildAppSettings();
				CreateServices();

				var _consoleInput = string.Empty;
				while (!string.Equals(_consoleInput, "exit", StringComparison.OrdinalIgnoreCase))
				{
					Console.WriteLine("Please enter distance in mega lights or type exit to finish");
					_consoleInput = Console.ReadLine();
					if (!double.TryParse(_consoleInput, out double _megaLights) || _megaLights < 1)
					{
						Console.WriteLine("Mega lights value must be a positive numeric value");
						continue;
					}
					var _pitStopsCalculatorService = c_serviceProvider.GetService<StarShipsPitStopCalculator.Domain.Workflow.IPitStopsCalculator>();
					var _starShipsPitStops = await _pitStopsCalculatorService.Execute(_megaLights);
					OutputResult(_starShipsPitStops);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}


		private static void BuildAppSettings()
		{
			var _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var _configuration = _configurationBuilder.Build();
			c_appSettings = new StarShipsPitStopCalculator.Domain.Types.Configuration.AppSettings(
				_configuration["starWarsUrl"],
				_configuration["starShipsEndpoint"]);
		}


		private static void CreateServices()
		{
		var _services = new ServiceCollection();
			_services
				.AddSingleton(c_appSettings)
				.AddTransient<StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever, StarShipsPitStopCalculator.Application.Infrastructure.StarShips.StarShipRetriever>()
				.AddTransient<StarShipsPitStopCalculator.Domain.Workflow.IPitStopsCalculator, StarShipsPitStopCalculator.Domain.Workflow.PitStopsCalculator>()
				.AddHttpClient<StarShipsPitStopCalculator.Application.Infrastructure.ICustomHttpClient, StarShipsPitStopCalculator.Application.Infrastructure.CustomHttpClient>();
			c_serviceProvider = _services.BuildServiceProvider();
		}


		private static void OutputResult(
			IEnumerable<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops> starShipsPitStops)
		{
			if (starShipsPitStops == null)
			{
				Console.WriteLine("Unable to retrieve any star ships");

				return;
			}

			starShipsPitStops.Where(starShipsPitStop => !string.Equals(starShipsPitStop.Stops, "unknown", StringComparison.OrdinalIgnoreCase))
				.ToList()
				.ForEach(starShipsPitStop => Console.WriteLine($"{starShipsPitStop.Name}: {starShipsPitStop.Stops}"));
			Console.WriteLine("MGLT unknown for the following star ships, unable to calculate stops!");
			starShipsPitStops.Where(starShipsPitStop => string.Equals(starShipsPitStop.Stops, "unknown", StringComparison.OrdinalIgnoreCase))
				.ToList()
				.ForEach(starShipsPitStop => Console.WriteLine($"{starShipsPitStop.Name}: {starShipsPitStop.Stops}"));

		}
	}
}
