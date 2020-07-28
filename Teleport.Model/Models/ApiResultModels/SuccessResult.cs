using System;
using System.Collections.Generic;
using System.Text;
using Teleport.Common.Enums;

namespace Teleport.Model.Models.ApiResultModels
{
    public class SuccessResult : Result
    {
        public SuccessResult(object Root, string Message) : base(true, Root, Message, ResultCodeEnum.Success)
        {
        }
    }
}
