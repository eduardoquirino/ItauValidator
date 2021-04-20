using System.Net;
using Itau.Validator.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Itau.Validator.ApiTool
{
    public class RestFulResult : JsonResult
    {
        public bool Success { get; set; }

        public RestFulResult(bool isValid, bool hasError = false)
            : base(new ValidatorResponse(){IsValid = isValid})
        {
            Success = isValid;
            if (!hasError)
            {
                this.StatusCode = isValid ? (int) HttpStatusCode.OK : (int) HttpStatusCode.BadRequest;
            }
            else
            {
                this.Success = false;
                this.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}
