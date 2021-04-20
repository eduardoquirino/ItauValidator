using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Itau.Validator.ApiTool;
using Itau.Validator.Model.Config;
using Microsoft.Extensions.Logging;

namespace Itau.Validator.Worker
{
    public class ValidatorWorker : IValidatorWorker
    {
        private readonly ILogger<ValidatorWorker> _logger;
        private readonly CustomConfiguration _customConfig;
        public ValidatorWorker(ILogger<ValidatorWorker> logger , CustomConfiguration  customConfig )
        {
            _logger = logger;
            _customConfig = customConfig;
        }

        public async Task<RestFulResult> ValidatePwd(string pwd)
        {
            var isValid = false;
            try
            {
                _logger.LogInformation("Validating...");

                await Task.Run(() =>
                {
                     isValid = IsValid(pwd);
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Exception has been thrown: " + e);
                
                return GetResponse(false,true);
            }

            return GetResponse(isValid);
        }

        private bool IsValid(string pwd)
        {
            if (_customConfig?.RegexPwd == null)
            {
                throw new ArgumentNullException();
            }

            var r = new Regex(_customConfig.RegexPwd);
            
            var m = r.Match(pwd);

            _logger.LogInformation($"Validated -  Result : {m.Success} ");

            return m.Success;
        }

        private RestFulResult GetResponse(bool isPwdValid, bool hasError = false)
        {
            return new RestFulResult(isPwdValid, hasError);
        }

    }
}
