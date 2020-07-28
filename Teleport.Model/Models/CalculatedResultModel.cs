using System;
using System.Collections.Generic;
using System.Text;

namespace Teleport.Model.Models
{
    public class CalculatedResultModel
    {
        public string FirstAirportName { get; set; }
        public string SecondAirportName { get; set; }
        public double Distance { get; set; }
        public string DistanceType { get; set; }
    }
}
