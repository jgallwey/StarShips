using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Application.IT.Infrastructure.StarShips
{
	[TestFixture]
	public class StarShipRetriever
	{
		private StarShipsPitStopCalculator.Application.Infrastructure.StarShips.StarShipRetriever c_SUT;
		private string c_url;


		[SetUp]
		public void Setup()
		{
			this.c_SUT = new StarShipsPitStopCalculator.Application.Infrastructure.StarShips.StarShipRetriever(
				new StarShipsPitStopCalculator.Application.Infrastructure.CustomHttpClient(new HttpClient()));
			this.c_url = "https://swapi.dev/api/starships/";
		}


		[Test]
		public async Task Retrieve_Success()
		{
			var _result = await this.c_SUT.Retrieve(this.c_url);

			Assert.IsNotNull(_result);
		}


		[Test]
		public async Task Retrieve_Failure_Invalid_URL()
		{
			var _result = await this.c_SUT.Retrieve("https://swapi.dev/api/star/");

			Assert.IsNull(_result);
		}
	}
}
