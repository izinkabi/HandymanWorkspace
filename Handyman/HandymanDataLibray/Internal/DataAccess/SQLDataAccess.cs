using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HandymanDataLibrary.Internal.DataAccess
{
    public class SQLDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public int SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var affectedrows = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return affectedrows;
            }

        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _transaction = _connection.BeginTransaction();

        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {

           List<T> rows = _connection.Query<T>(storedProcedure, parameters,
               commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }


        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {

            _connection.Execute(storedProcedure, parameters,
             commandType: CommandType.StoredProcedure, transaction: _transaction);
        
        }

        public void CommitTransaction()
        {

            _transaction?.Commit();
            _connection?.Close();

        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

        }

        public void Dispose()
        {
           //To Do
        }
    }
}
