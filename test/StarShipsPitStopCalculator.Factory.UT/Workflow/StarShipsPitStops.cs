using System;
using System.Collections.Generic;


namespace StarShipsPitStopCalculator.Factory.UT.Workflow
{
	public class StarShipsPitStops
	{
		public static IEnumerable<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops> Default()
		{
			return new List<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops>
			{
				new StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops("CR90 corvette","250"),
				new StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops("Star Destroyer","unknown")
			};
		}
	}
}
