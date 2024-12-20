using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message) { }

        public class RequirementException : AppException
        {
            public RequirementException(string description) : base(description) { }
        }

        public class BadRequestException : AppException
        {
            public BadRequestException(string description) : base($"Bad Request: {description}") { }
        }

        public class UnAuthorizedException : AppException
        {
            public UnAuthorizedException(string description = "Unauthorized") : base(description) { }
        }

        public class NotFoundException : AppException
        {
            public NotFoundException(string description) : base($"Not Found: {description}") { }
        }
    }
}

