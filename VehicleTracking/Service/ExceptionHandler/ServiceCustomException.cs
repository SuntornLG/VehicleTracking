using System;


namespace Service.ExceptionHandler
{
    [Serializable]
    public class ServiceCustomException : Exception
    {
        public ServiceCustomException(string message)
            : base(String.Format("An error found: {0}", message))
        {
        }
    }
}
