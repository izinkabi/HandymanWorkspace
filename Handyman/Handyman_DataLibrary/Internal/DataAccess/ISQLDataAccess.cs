namespace Handyman_DataLibrary.Internal.DataAccess
{
    public interface ISQLDataAccess
    {
        void CommitTransation();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataTransaction<T, U>(string storedProcedure, U parameters);
        void RollBackTransaction();
        int SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SaveDataTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}