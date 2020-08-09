namespace StarShipsPitStopCalculator.Domain.Types.Configuration
{
	public class AppSettings : StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings
	{
		public string StarWarsUrl { get; }
		public string StarShipsEndpoint { get; }


		public AppSettings(
			string starWarsUrl,
			string starShipsEndpoint)
		{
			this.StarWarsUrl = starWarsUrl;
			this.StarShipsEndpoint = starShipsEndpoint;
		}
	}
}
