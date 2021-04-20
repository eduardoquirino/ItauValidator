using System.Net;
using Itau.Validator.ApiTool;
using Itau.Validator.Controllers;
using Itau.Validator.Worker;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace UnitTest
{
    public class ValidatorController_Test
    {

        private readonly Mock<IValidatorWorker> validatorWorkerMock;
        private ValidatorController SUT;

        public ValidatorController_Test()
        {
            validatorWorkerMock = new Mock<IValidatorWorker>();
            this.SUT = new ValidatorController(NullLogger<ValidatorController>.Instance, validatorWorkerMock.Object);
        }

        [Theory]
        [InlineData("", false, HttpStatusCode.BadRequest)]
        [InlineData("aa", false, HttpStatusCode.BadRequest)]
        [InlineData("AAAbbbCc", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!foo", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!foA", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9 fok", false, HttpStatusCode.BadRequest)]
        [InlineData("AbTp9!fok", true, HttpStatusCode.OK)]
        public void ValidatorController_PWDTest(string pwd, bool expectedResult, HttpStatusCode expectedHttpCode)
        {

            #region Arrange

            validatorWorkerMock.Setup(x => x.ValidatePwd(It.IsAny<string>()))
                .ReturnsAsync(() => new RestFulResult(expectedResult)

                );

            #endregion

            #region Act

            var result = SUT.ValidatePwd(pwd);

            #endregion

            #region Assert

            Assert.Equal(expectedResult, result?.Result.Success);
            Assert.Equal((int) expectedHttpCode, result?.Result.StatusCode);

            #endregion

        }
    }
}
