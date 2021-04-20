using System.Threading.Tasks;
using Itau.Validator.ApiTool;

namespace Itau.Validator.Worker
{
    public interface IValidatorWorker
    {
        Task<RestFulResult> ValidatePwd(string pwd);
    }
}
