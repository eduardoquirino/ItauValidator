using System;

namespace Itau.Validator.Contract
{
    [Serializable]
    public class ValidatorResponse
    {
        public bool IsValid { get; set; }
    }
}
