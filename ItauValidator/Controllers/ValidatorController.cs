using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Itau.Validator.ApiTool;
using Itau.Validator.Worker;

namespace Itau.Validator.Controllers
{
    [ApiController]
    [Route("/api/v1/")]
    public class ValidatorController : ControllerBase
    {
        
        private readonly ILogger<ValidatorController> _logger;
        private readonly IValidatorWorker _validatorWorker;

        public ValidatorController(ILogger<ValidatorController> logger, IValidatorWorker validatorWorker)
        {
            _logger = logger;
            _validatorWorker = validatorWorker;
        }

        [HttpGet("validator/pwd/{pwd}")]
        public async Task<RestFulResult> ValidatePwd([FromRoute] string pwd)
        {
            _logger.LogInformation("Request received for validation");
            
            var response = await _validatorWorker.ValidatePwd(pwd);
            return response;
        }
    }
}
