using System;
using System.Collections.Generic;
using System.Text;

namespace Teleport.Model.Models
{
    public class CalculateRequestModel
    {
        public string FirstAirportCode { get; set; }
        public string SecondAirportCode { get; set; }
        public string ReturnValueType { get; set; }
    }
}
