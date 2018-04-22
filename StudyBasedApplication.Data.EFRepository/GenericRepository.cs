using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyBasedApplication.Data.Repository;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace StudyBasedApplication.Data.EFRepository
{
    internal class GenericRepository<TObject> : IRepository<TObject> where TObject : class
    {
        private DbContext _context = null;
        private DbSet<TObject> _entitySet = null;

        public GenericRepository(DbContext Context)
        {
            this._context = Context;
            this._entitySet = Context.Set<TObject>();
        }

      
        protected DbSet<TObject> DbSet
        {
            get
            { return _entitySet; }
        }

        public virtual IEnumerable<TObject> All()
        {
            try
            {
                return DbSet.AsEnumerable<TObject>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate)
        {
            try
            {
                return DbSet.Where(predicate).AsEnumerable<TObject>();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;
            try
            {
                query = query.Where(predicate);

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                { query = query.Include(includeProperty); }

                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual TObject Find(params object[] keys)
        {
            try
            {

                return DbSet.Find(keys);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            try
            {
                return DbSet.FirstOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;
            try
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                { query = query.Include(includeProperty); }

                return query.FirstOrDefault(predicate);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual TObject Create(TObject entry)
        {
            try
            {
                var newEntry = DbSet.Add(entry);
                Save();
                return newEntry;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public virtual void Update(TObject entry)
        {
            try
            {
                DbSet.Attach(entry);
                _context.Entry(entry).State = EntityState.Modified;
                Save();
            }
            catch (Exception e)
            {
                throw new Exception("Not able to update");
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TObject entityToDelete = DbSet.Find(id);
                Delete(entityToDelete);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to delete");
            }
        }
        public virtual void Delete(TObject entry)
        {
            try
            {
                if (_context.Entry(entry).State == EntityState.Detached)
                {
                    DbSet.Attach(entry);
                }
                DbSet.Remove(entry);
                Save();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to delete");
            }
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }


        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (SqlException e)
            {
                throw e;
            }
            
            catch (Exception e)
            {
                throw new Exception("Unable to save changes");
            }
        }
    }
}
