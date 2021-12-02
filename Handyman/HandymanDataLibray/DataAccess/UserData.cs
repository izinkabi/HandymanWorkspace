
using HandymanDataLibrary.Models;
using HandymanDataLibray.DataAccess.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    public class UserData
    {
        /*Getting a user by ID*/
        public UserModel GetUserById(UserModel userModel)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { Id = userModel.Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "DefaultConnection");

            return output.First();

        }

        /*Getting Users by username*/
       
        public List<UserModel> GetUserByUsername(string email)
        {
            var p = new { Email = email };

            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "HandymanDB");
            return output;
        }

        /*Posting a new user*/
        public void PostUser(UserModel val)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<UserModel>("dbo.spUserInsert", val, "HandymanDB");
        }


        public void RegisterUser(UserModel userModel)
        {

                    SQLDataAccess sql = new SQLDataAccess();
            
                
                    //var p = new { Email = email };
                    UserModel output = new UserModel();
                    output = sql.LoadData<UserModel, dynamic>("dbo.spASPUserLookUp", new{ email = userModel.Email}, "DefaultConnection").First();

                    //var user = new {UserName = output.Username, Id = output.Id, Email = output.Email };

                    int rowsaffected =  sql.SaveData<UserModel>("dbo.spUserInsert", output, "HandymanDB");
                
            
        }

        /*Modify and Delete are to be implemented*/
    }

    
}
