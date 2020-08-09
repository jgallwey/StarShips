using System.Collections.Generic;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Domain.Infrastructure
{
	public interface IStarShipRetriever
	{
		Task<IEnumerable<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip>> Retrieve(
			string url);
	}
}
