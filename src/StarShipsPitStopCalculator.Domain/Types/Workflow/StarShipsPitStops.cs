namespace StarShipsPitStopCalculator.Domain.Types.Workflow
{
	public class StarShipsPitStops
	{
		public string Name { get; }
		public string Stops { get; }


		public StarShipsPitStops(
			string name,
			string stops)
		{
			this.Name = name;
			this.Stops = stops;
		}
	}
}