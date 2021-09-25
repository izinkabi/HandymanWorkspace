/*This is a user's profile data class*/
//connecting the to the database 
using HandymanDataLibrary.Internal.DataAccess;
using HandymanDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    public class ProfileData
    {
     
        ///*Getting profile by Id*/
        //public ProfileModel GetProfileById(int Id)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();

        //    var p = new { Id = Id };

        //    var output = sql.LoadData<ProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB");

        //    return output.First();

        //}

        ///*Getting Profiles by username*/

        //public List<ProfileModel> GetProfileByUserId(string userId)
        //{
        //    var p = new { Email = userId };

        //    SQLDataAccess sql = new SQLDataAccess();
        //    var output = sql.LoadData<ProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB");
        //    return output;
        //}

        ///*Posting a new Profile*/
        //public void PostProfile(ProfileModel val)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();
        //    sql.SaveData<ProfileModel>("dbo.spProfileInsert", val, "HandymanDB");
        //}

    }
}
