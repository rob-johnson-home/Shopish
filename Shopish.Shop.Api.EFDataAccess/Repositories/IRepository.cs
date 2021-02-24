using Microsoft.Data.SqlClient;
using Shopish.Shop.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

        // for stored procedures 

        IEnumerable<T> ExecuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null);
        //void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null);
        //IEnumerable<T> ExecuteReader(string commandText, CommandType commandType, SqlParameter[] parameters = null);

    }


}