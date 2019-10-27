using System;

namespace Designer.Extensions
{
    public class ServiceException : Exception
    {
        public ServiceException(string msg) : base(msg)
        {
        }
    }
}