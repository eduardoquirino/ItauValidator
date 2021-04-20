using System.Net;
using Itau.Validator.Model.Config;
using Itau.Validator.Worker;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace UnitTest
{
    public class ValidatorWorkerTest
    {
        [Theory]
        [InlineData("", false, HttpStatusCode.BadRequest)]
        [InlineData("aa", false,HttpStatusCode.BadRequest)]
        [InlineData("AAAbbbCc", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!foo", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!foA", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9 fok", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!fok", true, HttpStatusCode.OK)]
         public void ValidatorWorker_PWDTest(string pwd, bool expectedResult, HttpStatusCode expectedHttpCode)
        {

            #region Arrange
            var validatorWorker = GetValidatorWorkerMock();
            #endregion

            #region Act
            var response = validatorWorker.ValidatePwd(pwd);
            #endregion

            #region Assert
            Assert.Equal(expectedResult, response.Result.Success);
            Assert.Equal((int) expectedHttpCode, response.Result.StatusCode);
            #endregion

        }
        [Theory]
        [InlineData("AbTp9!fok", true, HttpStatusCode.OK)]
        [InlineData("AbTp9!fok", false, HttpStatusCode.InternalServerError, true)]
        public void ValidatorWorker_ExceptionTest(string pwd, bool expectedResult, HttpStatusCode expectedHttpCode,bool forceException = false)
        {

            #region Arrange
            var validatorWorker = GetValidatorWorkerMock(forceException);
            #endregion

            #region Act
            var response = validatorWorker.ValidatePwd(pwd);
            #endregion

            #region Assert
            Assert.Equal(expectedResult, response.Result.Success);
            Assert.Equal((int)expectedHttpCode, response.Result.StatusCode);
            #endregion

        }

        private ValidatorWorker GetValidatorWorkerMock(bool forceException = false)
        {
            var logger = NullLogger<ValidatorWorker>.Instance;

            var config = !forceException ? GetCustomConfiguration() : null;

            return new ValidatorWorker(logger,  config);
        }

        private CustomConfiguration GetCustomConfiguration()
        {
           return   new CustomConfiguration()
            {
                RegexPwd = "^(?:([A-Za-z0-9#?!+@$%^&*-])(?!.*\\1)){9,}$"
            };
        }
    }
}
