using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess.Internal
{
    //Had to implement a IDisposable to use the Using stament when starting a transaction
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
        
        //Start Transaction 
        //Open connection 
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            IsClosed = false;
        }

        //Save Data Transaction 
        public void SaveDataTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        //Load Data Transattion 
        public List<T> LoadDataTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }

        private bool IsClosed = false;
        public void CommitTransation()
        {
            _transaction?.Commit();
            _connection?.Close();
            IsClosed = true;
        }

        public void RollBackTransaction()
        {
            _transaction.Rollback();
            _connection?.Close();
            IsClosed = true;

        }

        public void Dispose()
        {
            if (IsClosed == false)
            {
                try
                {
                    CommitTransation();
                }
                catch
                {

                    //TODO Log issue 
                }
            }

            _transaction = null;
            _connection = null;
        }
        //Open connect/start transaction
        //load using transaction
        //save using transaction
        //Close connection/stop transtion method!!!
        //Dispose
    }
}

