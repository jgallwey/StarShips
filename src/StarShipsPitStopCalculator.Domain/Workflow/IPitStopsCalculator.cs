using System.Collections.Generic;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Domain.Workflow
{
	public interface IPitStopsCalculator
	{
		Task<IEnumerable<StarShipsPitStopCalculator.Domain.Types.Workflow.StarShipsPitStops>> Execute(
			double megaLights);
	}
}
