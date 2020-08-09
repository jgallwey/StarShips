namespace StarShipsPitStopCalculator.Domain.Types.AggregateRoot
{
	public class StarShip
	{
		public string Name { get; }
		public string MGLT { get; }


		public StarShip(
			string name,
			string MGLT)
		{
			this.Name = name;
			this.MGLT = MGLT;
		}
	}
}
