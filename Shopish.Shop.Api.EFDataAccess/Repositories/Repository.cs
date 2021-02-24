using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shopish.Shop.Api.Domain.Model;
using Shopish.Shop.Api.EFDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ShopDbContext _dbContext;
        //public DbSet<T> dbSet { get; set; }

        public Repository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public virtual IEnumerable<T> List()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                   .Where(predicate)
                   .AsEnumerable();
        }
        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        // stored procedures
        

            public IEnumerable<T> ExecuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
            {
                if (commandType == CommandType.Text)
                {
                    return SqlQuery(sqlQuery, parameters);
                }
                else if (commandType == CommandType.StoredProcedure)
                {
                    return StoredProcedure(sqlQuery, parameters);
                }

                return null;
            }

                      

            private IEnumerable<T> SqlQuery(string sqlQuery, SqlParameter[] parameters = null)
            {
                if (parameters != null && parameters.Any())
                {
                    var parameterNames = new string[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameterNames[i] = parameters[i].ParameterName;
                    }

                    var result = _dbContext.Set<T>().FromSqlRaw(string.Format("{0}", sqlQuery, string.Join(",", parameterNames), parameters));
                    return result.ToList();
                }
                else
                {
                    var result = _dbContext.Set<T>().FromSqlRaw(sqlQuery);
                    return result.ToList();
                }
            }

            private IEnumerable<T> StoredProcedure(string storedProcedureName, SqlParameter[] parameters = null)
            {
                if (parameters != null && parameters.Any())
                {
                    var parameterNames = new string[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parameterNames[i] = parameters[i].ParameterName;
                    }

                    var result = _dbContext.Set<T>().FromSqlRaw(string.Format("EXEC {0} {1}", storedProcedureName, string.Join(",", parameterNames), parameters));
                    return result.ToList();
                }
                else
                {
                    var result = _dbContext.Set<T>().FromSqlRaw(string.Format("EXEC {0}", storedProcedureName));
                    return result.ToList();
                }
            }
        }
}


