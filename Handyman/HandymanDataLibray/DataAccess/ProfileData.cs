using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    public class ProfileData
    {

        /*Getting profile by Id*/
        public ProfileModel GetProfileById(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            ///*Getting profile by Id*

            var p = new { Id = Id };
            //    var p = new { Id = Id };

            var output = sql.LoadData<ProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB");
            
            return output.First();
            

        }
        

        /*Getting Profiles by username*/
        ///*Getting Profiles by username*/

        public ProfileModel GetProfileByUserId(string userId)
        {
            var p = new { Email = userId };
           

            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB").First();
            return output;
        }
       

        /*Posting a new Profile and a new Address*/
        public void PostProfile(ProfileModel val)
        {
            

            using (SQLDataAccess sql = new SQLDataAccess())
            { 
       
                try
                {
                    //Save the Address model
                    sql.StartTransaction("HandymanDB");
                    sql.SaveDataTransaction("dbo.spAddressInsert", new { StreetName = val.Address.StreetName, City = val.Address.City, PostalCode = val.Address.PostalCode, HouseNumber = val.Address.HouseNumber,Surburb=val.Address.Surburb });
                    //
                    
                    //Lookup for a AddressId
                    var HomeAddressId = sql.LoadDataTransaction<int, dynamic>("dbo.spAddressLookUp", new { HouseNumber=val.Address.HouseNumber,StreetName=val.Address.StreetName }).FirstOrDefault();
                   
                    //Save ProfileModel
                    sql.SaveDataTransaction("dbo.spProfileInsert", new { Name=val.Name,Surname=val.Surname,PhoneNumber=val.PhoneNumber,AddressId=HomeAddressId,DateOfBirth=val.DateofBirth,UserId=val.UserId});
                    //Commit the transaction
                    sql.CommitTransation();
                }
                catch
                {
                    //Roll back the transaction if anything goes wrong
                    sql.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}