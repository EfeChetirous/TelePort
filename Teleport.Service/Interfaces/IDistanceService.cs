using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teleport.Model.Models;
using Teleport.Model.Models.ApiResultModels;

namespace Teleport.Service.Interfaces
{
    public interface IDistanceService
    {
        public Task<Result> CalculateDistanceAsync(CalculateRequestModel rqModel);
    }
}
