using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class TodoData
    {
        //Getting the todo list by the orderId
        public IEnumerable<TodoModel> GetTodoListByOrderId(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();


            var p = new { OrderId = Id };

            var output = sql.LoadData<TodoModel, dynamic>("Customer.spTodoItemsLookUpByOrderId", p, "HandymanDB");

            return output;
        }




        //Post each todo-item in the doto-list of an Order
        public void PostTodo(List<TodoModel> todoList)
        {
            SQLDataAccess sql = new SQLDataAccess();
            try 
            { 
            sql.StartTransaction("HandymanDB");
            foreach (TodoModel todo in todoList)
            {
                sql.SaveDataTransaction("Customer.spTodoInsert", new
                {
                    ItemName = todo.ItemName,
                    OrderId = todo.OrderId,
                    Type = todo.Type,
                    Status = todo.Status,
                    Description = todo.Description

                });
            }
                sql.CommitTransation();
            }catch (Exception ex)
            {
                sql.RollBackTransaction();
               throw new Exception(ex.Message);
            }

}

        ////Updating the order
        public void UpdateTodo(TodoModel todoUpdate)
        {

            SQLDataAccess sql = new SQLDataAccess();


            sql.SaveData("Customer.spTodoUpdate", new { StartDate = todoUpdate.StartDate, EndDate = todoUpdate.EndDate }, "HandymanDB");

        }

        
        //    public void DeleteTodo(int Id)
        //    {
        //        SQLDataAccess sql = new SQLDataAccess();

        //        try
        //        {
        //            sql.StartTransaction("HandymanDB");

        //            //Delete the request first
        //            sql.SaveDataTransaction("Customer.spTodoDelete", new { OrderId = Id });

        //            //Go on and delete the order
        //            sql.SaveDataTransaction("Customer.spOrderDelete", new { Id = Id });

        //            sql.CommitTransation();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}
    }
}
