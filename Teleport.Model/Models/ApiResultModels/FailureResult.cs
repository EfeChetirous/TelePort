using System;
using System.Collections.Generic;
using System.Text;
using Teleport.Common.Enums;

namespace Teleport.Model.Models.ApiResultModels
{
    public class FailureResult : Result
    {

        public FailureResult() : base(false, null, "An error has been occurred!", ResultCodeEnum.Failure)
        {

        }

        public FailureResult(string message) : base(false, null, message, ResultCodeEnum.Failure)
        {

        }

        public FailureResult(ResultCodeEnum resultCodeEnum) : base(false, null, "An error has been ocurred!", resultCodeEnum)
        {

        }
    }
}
