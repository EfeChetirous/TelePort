using GeoCoordinatePortable;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Teleport.Common;
using Teleport.Common.Enums;
using Teleport.Model.Models;
using Teleport.Model.Models.ApiResultModels;
using Teleport.Service.Interfaces;

namespace Teleport.Service.Services
{
    public class DistanceService : IDistanceService
    {
        private readonly AppSettings _appSettings;
        public DistanceService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// calculate two airport measure. Default return type value is mile. ReturnValueType property is optional. 
        /// you can set as km or mile or send null.
        /// </summary>
        /// <param name="rqModel"></param>
        /// <returns></returns>
        public async Task<Result> CalculateDistanceAsync(CalculateRequestModel rqModel)
        {
            try
            {
                CalculatedResultModel calcResultModel = new CalculatedResultModel();
                //validate airportcodes
                if (string.IsNullOrWhiteSpace(rqModel.FirstAirportCode) || string.IsNullOrWhiteSpace(rqModel.SecondAirportCode))
                {
                    return new FailureResult("First and second airportcodes are mandatory fields.");
                }

                var firstAirport = await GetAirportInfoAsync(rqModel.FirstAirportCode);
                var secondAirport = await GetAirportInfoAsync(rqModel.SecondAirportCode);
                calcResultModel.Distance = GetDistance(firstAirport, secondAirport, rqModel.ReturnValueType?.ToLower());
                calcResultModel.FirstAirportName = firstAirport.Name;
                calcResultModel.SecondAirportName = secondAirport.Name;
                calcResultModel.DistanceType = rqModel.ReturnValueType?.ToLower() == "km" ? "km" : "mile";

                return new SuccessResult(calcResultModel, "Success");
            }
            catch (Exception ex)
            {
                return new FailureResult(ex.Message);
            }
        }

        private double GetDistance(AirportInfoModel firstAirport, AirportInfoModel secondAirport, string returnValType = Constants.Mile)
        {
            try
            {
                GeoCoordinate pin1 = new GeoCoordinate(firstAirport.Location.Latitude, firstAirport.Location.Longitude);
                GeoCoordinate pin2 = new GeoCoordinate(secondAirport.Location.Latitude, secondAirport.Location.Longitude);
                
                //default return type is meter from GetDistanceTo.
                double measuredDistance = pin1.GetDistanceTo(pin2);
                if (measuredDistance != 0)
                {
                    measuredDistance = measuredDistance / 1000; // convert to km.
                }
                else
                {
                    return 0;
                }

                if (returnValType != Constants.Kilometer)
                {
                    return measuredDistance.ConvertKilometersToMiles();
                }
                return measuredDistance;
            }
            catch (Exception ex)
            {
                throw new Exception("An error is occured while calculating measure.");
            }
        }

        private async Task<AirportInfoModel> GetAirportInfoAsync(string airportCode)
        {
            AirportInfoModel airportInfo = new AirportInfoModel();
            var response = string.Empty;
            string endPoint = $"{_appSettings.TelePortDevUrl}{airportCode.ToUpper(new CultureInfo("en-US", false))}";
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(endPoint);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                    airportInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<AirportInfoModel>(response);
                }
                else
                {
                    throw new Exception("An error is occured while fetching airport info. Please check airport IATA code.");
                }
            }
            return airportInfo;
        }
    }
}
