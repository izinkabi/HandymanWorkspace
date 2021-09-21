using HandymanDataLibrary.Internal.DataAccess;
using HandymanDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    public class UserData
    {
        /*Getting a users by ID*/
        public List<UserModel> GetUserById(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "DefaultConnection");

            return output;

        }

        /*Getting Users by username*/
       
        public List<UserModel> GetUserByUsername(string email)
        {
            var p = new { Email = email };

            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<UserModel, dynamic>("dbo.spASPUserLookUp", p, "DefaultConnection");
            return output;
        }

        /*Posting a new user*/
        public void PostUser(UserModel val)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<UserModel>("dbo.spUserInsert", val, "DefaultConnection");
        }

        /*Modify and Delete are to be implemented*/
    }

    
}
