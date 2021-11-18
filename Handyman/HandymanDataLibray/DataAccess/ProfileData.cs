﻿using HandymanDataLibray.DataAccess.Internal;
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
        

        /*Getting Profiles by UserId*/
  
        public ProfileModel GetProfileByUserId(string userId)
        {
            var p = new { UserId = userId };
           

            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<aliasProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB").First();

            /*Populating the lookup profile stored-procedure data*/
            var tempProfile = new ProfileModel();
            tempProfile.Address = new ProfileModel.AddressModel();
            tempProfile.Id = output.Id;
            tempProfile.Name = output.Name;
            tempProfile.Surname = output.Surname;
            tempProfile.PhoneNumber = output.PhoneNumber;
            tempProfile.UserId = output.UserId;
            tempProfile.DateofBirth = output.DateofBirth;
            tempProfile.AddressId = output.AddressId;
            /*Address population*/
            tempProfile.Address.Id = output.AddressId;
            tempProfile.Address.StreetName = output.StreetName;
            tempProfile.Address.HouseNumber = output.HouseNumber;
            tempProfile.Address.Surburb = output.Surburb;
            tempProfile.Address.PostalCode = output.PostalCode;
            tempProfile.Address.City = output.City;

            return tempProfile;
        }
       

        /*Posting a new Profile and a new Address Using a Transaction*/
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
                    sql.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}