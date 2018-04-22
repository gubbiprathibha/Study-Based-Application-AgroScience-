using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyBasedApplication.Business
{
    public interface IGenericGetter<TType>
    {
        IEnumerable<TType> GetAll();
    }
}
