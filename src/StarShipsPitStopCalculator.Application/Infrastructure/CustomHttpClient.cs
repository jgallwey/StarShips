using System.Net.Http;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Application.Infrastructure
{
	public class CustomHttpClient : StarShipsPitStopCalculator.Application.Infrastructure.ICustomHttpClient
	{
		private readonly HttpClient c_httpClient;


		public CustomHttpClient(
			HttpClient httpClient)
		{
			this.c_httpClient = httpClient;
		}


		public async Task<HttpResponseMessage> RetrieveStarShips(
			string url)
		{
			return await this.c_httpClient.GetAsync(url);
		}
	}
}