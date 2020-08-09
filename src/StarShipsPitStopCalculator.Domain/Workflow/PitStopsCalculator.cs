using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Domain.Workflow
{
	public class PitStopsCalculator : StarShipsPitStopCalculator.Domain.Workflow.IPitStopsCalculator
	{
		private readonly StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings c_appSettings;
		private readonly StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever c_starShipRetriever;


		public PitStopsCalculator(
			StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings appSettings,
			StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever starShipRetriever)
		{
			this.c_appSettings = appSettings;
			this.c_starShipRetriever = starShipRetriever;
		}


		public async Task<IEnumerable<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops>> Execute(
			double megaLights)
		{
			var _starShips = await this.c_starShipRetriever.Retrieve($"{this.c_appSettings.StarWarsUrl}/{this.c_appSettings.StarShipsEndpoint}/");
			if (_starShips == null) { return null; }

			return this.BuildStarShipsPitStops(_starShips, megaLights);
		}


		private IEnumerable<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops> BuildStarShipsPitStops(
			IEnumerable<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip> starShips,
			double megaLights)
		{
			return starShips.Select(starShip =>
				new StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops(starShip.Name, this.CalculatePitStops(starShip, megaLights)));
		}


		private string CalculatePitStops(
			StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip starShip,
			double megaLights)
		{
			if (!double.TryParse(starShip.MGLT, out double _MGLT)) { return starShip.MGLT; }

			return _MGLT < 1 ? "unknown" : Math.Floor(megaLights / _MGLT).ToString();
		}

	}
}
