using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;


namespace Handyman_DataLibrary.DataAccess.Query
{
    public class OrderData : IOrderData
    {
        ISQLDataAccess _dataAccess;
        public OrderData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        //Get the consumer's orders and their respective tasks
        public List<TaskModel> GetConsumerOrderAndTasks(string consumerID)
        {
            List<TaskModel> orders = new()!;
            try
            {
               orders = _dataAccess.LoadData<TaskModel, dynamic>("Request.spOrderLookUp_ByConsumerId_OrderByDateCreated",
                        new { consumerID = consumerID }, "HandymanDB");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
            return orders;
        }

        public void InsertOrder()
        {
            try
            {
                _dataAccess.SaveData("Request.spOrderInsert", new { }, "HandymanDB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(OrderModel orderUpdate)
        {
            try
            {
                _dataAccess.SaveData("Request.spOrderInsert" +
                    "", new { }, "HandymanDB");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } 
    }
}
