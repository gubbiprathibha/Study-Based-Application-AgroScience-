using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyBasedApplication.Exceptions
{
    public class StatusMappingException : ApplicationException
    {
        public StatusMappingException()
        { }

        public StatusMappingException(string msg)
            : base(msg)
        { }
    }
}
