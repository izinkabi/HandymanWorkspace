using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    public class UserData
    {
        /*Getting a user by ID*/
        //public UserModel GetUserById(UserModel userModel)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();

        //    var p = new { Id = userModel.Id };

        //    var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "DefaultConnection");


        //    return output.First();

        //}

        ///*Getting Users by username*/
       
        //public List<UserModel> GetUserByUsername(string email)
        //{
        //    var p = new { Email = email };

        //    SQLDataAccess sql = new SQLDataAccess();
        //    var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "HandymanDB");
        //    return output;
        //}

        ///*Posting a new user*/
        //public void PostUser(UserModel val)
        //{
          
        //    SQLDataAccess sql = new SQLDataAccess();
        //    sql.SaveData("dbo.spUserInsert", new { Email = val.Email, Username = val.Username, Id = val.Id }, "HandymanDB");
        //}


        //public void RegisterUser(UserModel userModel)
        //{

        //            SQLDataAccess sql = new SQLDataAccess();
            
               
        //            //var p = new { Email = email };
        //            UserModel output = new UserModel();
        //    sql.StartTransaction("DefaultConnection");
        //    output = sql.LoadData<UserModel, dynamic>("dbo.spASPUserLookUp", new { email = userModel.Email }, "DefaultConnection").First();
        //    //

        //    try
        //    {

        //        if (userModel.UserRole == "Customer")//Customer user-role creation
        //        {

        //            var userrole = sql.LoadData<RoleModel, dynamic>("dbo.spAspNetRolesLookUp", new { RoleName = userModel.UserRole }, "DefaultConnection").First();

        //            int rowaffected = sql.SaveData("dbo.spAspNetUserRolesInsert", new { UserId = output.Id, RoleId = userrole.Id }, "DefaultConnection");

        //        }
        //        else if (userModel.UserRole == "ServiceProvider") //Provider user-role creation
        //        {
        //            var userrole = sql.LoadData<RoleModel, dynamic>("dbo.spAspNetRolesLookUp", new { RoleName = userModel.UserRole }, "DefaultConnection").First();

        //            int rowaffected = sql.SaveData("dbo.spAspNetUserRolesInsert", new { UserId = output.Id, RoleId = userrole.Id }, "DefaultConnection");
        //        }
        //        //Commit the transaction
        //        sql.CommitTransation();
        //    }
        //    catch(Exception ex)
        //    {
        //        //Roll back the transaction if anything goes wrong
        //        sql.RollBackTransaction();
        //        throw new Exception(ex.Message);
        //    }


        //    //var user = new {UserName = output.Username, Id = output.Id, Email = output.Email };

        //    int rowsaffected =  sql.SaveData("dbo.spUserInsert", new { Id=output.Id,Email = output.Email,Username= output.Username }, "HandymanDB");
                    
                
            
        //}

        /*Modify and Delete are to be implemented*/
    }

    
}
