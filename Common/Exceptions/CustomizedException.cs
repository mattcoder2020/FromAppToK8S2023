using System;

namespace Common.Exceptions
{
    public class CustomizedException <T> : ApplicationException 
    {
        public CustomizedException(string message) : base(typeof(T).FullName + "： " +message)
        {                        
        }

        public CustomizedException(string message, System.Exception innerException) : base(typeof(T).FullName + "： " + message, innerException)
        {
        }

    }
}
