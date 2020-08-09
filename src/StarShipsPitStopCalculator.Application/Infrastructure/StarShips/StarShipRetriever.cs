﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace StarShipsPitStopCalculator.Application.Infrastructure.StarShips
{
	public class StarShipRetriever : StarShipsPitStopCalculator.Domain.Infrastructure.IStarShipRetriever
	{
		private readonly StarShipsPitStopCalculator.Application.Infrastructure.ICustomHttpClient c_customHttpClient;


		private List<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip> c_starShips;


		public StarShipRetriever(
			StarShipsPitStopCalculator.Application.Infrastructure.ICustomHttpClient customHttpClient)
		{
			this.c_customHttpClient = customHttpClient;
		}


		public async Task<IEnumerable<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip>> Retrieve(
			string url)
		{
			this.c_starShips = new List<StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip>();
			var _url = url;
			while (!String.IsNullOrEmpty(_url))
			{
				var _response = await this.c_customHttpClient.RetrieveStarShips(_url);
				if (_response.StatusCode != HttpStatusCode.OK) { return null; }

				var _responseBody = await _response.Content.ReadAsStringAsync();
				var _starShips = JsonSerializer.Deserialize<StarShipsPitStopCalculator.Application.Infrastructure.StarShips.Types.StarShips>(_responseBody);
				this.MapToDomain(_starShips);
				_url = _starShips.next;
			}

			return this.c_starShips;
		}


		private void MapToDomain(
			StarShipsPitStopCalculator.Application.Infrastructure.StarShips.Types.StarShips starShips)
		{
			starShips.results.ToList().ForEach(result =>
				this.c_starShips.Add( new StarShipsPitStopCalculator.Domain.Types.AggregateRoot.StarShip(result.name, result.MGLT)));
		}
	}
}
