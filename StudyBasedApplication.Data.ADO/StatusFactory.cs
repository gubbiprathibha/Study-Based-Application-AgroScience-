using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyBasedApplication.Data.ADO
{
    public class StatusFactory
    {
            private static StatusFactory statusFactoryInstance = new StatusFactory();
            private StatusFactory()
            {
            }
            public static StatusFactory GetInstance()
            {
                return statusFactoryInstance;
            }
            public IStatusRepository GetStatusInstance()
            {
                return new StatusRepository();
            }
        
    }
}
