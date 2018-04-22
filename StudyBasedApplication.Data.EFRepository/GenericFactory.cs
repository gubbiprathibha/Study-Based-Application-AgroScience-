using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.Repository;

namespace StudyBasedApplication.Data.EFRepository
{
    public class GenericFactory<TObject> where TObject: class
    {
        private static GenericFactory<TObject> FactoryInstance = new GenericFactory<TObject>();
        private GenericFactory()
        {
        }
        public static GenericFactory<TObject> GetInstance()
        {
            return FactoryInstance;
        }
        public IRepository<TObject> GetObject()
        {
            PrimaryDBContext db = new PrimaryDBContext();
            return new GenericRepository<TObject>(db);
        }
    }
}
