using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Domain.UT.Workflow
{
	[TestFixture]
	public class PitStopsCalculator
	{
		private StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings c_appSettings;
		private StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever c_starShipRetriever;
		private double c_megaLights;
		private StarShipsPitStopCalculator.Domain.Workflow.PitStopsCalculator c_SUT;


		[SetUp]
		public void SetUp()
		{
			this.c_megaLights = 5000;
			this.c_appSettings = Substitute.For<StarShipsPitStopCalculator.Domain.Types.Configuration.IAppSettings>();
			this.c_starShipRetriever = Substitute.For<StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever>();
			this.c_SUT = new StarShipsPitStopCalculator.Domain.Workflow.PitStopsCalculator(c_appSettings, c_starShipRetriever);
		}


		[Test]
		public async Task Execute_Success()
		{
			var _StarShips = StarShipsPitStopCalculator.Factory.UT.AggregateRoot.StarShip.Default();
			this.c_starShipRetriever.Retrieve("URL").ReturnsForAnyArgs(_StarShips);
			var _expectedRatesResponse = StarShipsPitStopCalculator.Factory.UT.Workflow.StarShipsPitStops.Default();

			var _result = await this.c_SUT.Execute(this.c_megaLights);

			Assert.AreEqual(_expectedRatesResponse.Count(), _result.Count());
			Assert.AreEqual(_expectedRatesResponse.First().Name, _result.First().Name);
			Assert.AreEqual(_expectedRatesResponse.First().Stops, _result.First().Stops);
			Assert.AreEqual(_expectedRatesResponse.Last().Name, _result.Last().Name);
			Assert.AreEqual(_expectedRatesResponse.Last().Stops, _result.Last().Stops);
		}


		[Test]
		public async Task Execute_Success_No_StarShips_Found()
		{
			this.c_starShipRetriever.Retrieve("URL").ReturnsForAnyArgs(new List<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip>());

			var _result = await this.c_SUT.Execute(this.c_megaLights);

			Assert.AreEqual(0, _result.Count());
		}
	}
}
