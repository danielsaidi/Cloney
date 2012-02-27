using System;

namespace Cloney.Core.SubRoutines
{
    public class SubRoutineException : Exception
    {
        public SubRoutineException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }


        public string ErrorMessage { get; private set; }
    }
}
