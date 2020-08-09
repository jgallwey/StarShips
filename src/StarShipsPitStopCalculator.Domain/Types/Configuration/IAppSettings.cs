namespace StarShipsPitStopCalculator.Domain.Types.Configuration
{
	public interface IAppSettings
	{
		string StarWarsUrl { get; }
		string StarShipsEndpoint { get; }
	}
}
