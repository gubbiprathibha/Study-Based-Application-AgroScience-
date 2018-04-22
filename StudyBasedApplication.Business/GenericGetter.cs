using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Business
{
    internal class GenericGetter<TType> : IGenericGetter<TType> where TType : class
    {

        public IEnumerable<TType> GetAll()
        {

           
            IRepository<TType> genericrepo = GenericFactory<TType>.GetInstance().GetObject();
            return genericrepo.All();
        }
    }
}
