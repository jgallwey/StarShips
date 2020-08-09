using System.Collections.Generic;


namespace StarShipsPitStopCalculator.Application.Infrastructure.StarShips.Types
{
    public class StarShips
    {
        public string next { get; set; }
        public List<Result> results { get; set; }
    }


    public class Result
    {
        public string name { get; set; }
        public string MGLT { get; set; }
    }
}
