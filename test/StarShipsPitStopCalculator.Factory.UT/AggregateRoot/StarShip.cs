using System;
using System.Collections.Generic;


namespace StarShipsPitStopCalculator.Factory.UT.AggregateRoot
{
	public class StarShip
	{
		public static IEnumerable<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip> Default()
		{
			return new List<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip>
			{
				new StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip("CR90 corvette","20"),
				new StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip("Star Destroyer","unknown")
			};
		}
	}
}
