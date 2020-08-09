using System.Net.Http;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Application.Infrastructure
{
	public interface ICustomHttpClient
	{
		Task<HttpResponseMessage> RetrieveStarShips(
			string url);
	}
}
