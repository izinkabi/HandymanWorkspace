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
        private ProfileModel.AddressModel AddressModel;
        private List<ProviderAddress> addresses;
        private SQLDataAccess sql;

        /*Getting profile by Id*/
        public ProfileModel GetProfileById(int Id)
        {
             sql = new SQLDataAccess();
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
            var tempProfile = new ProfileModel();

           sql = new SQLDataAccess();
            try
            {
                var output = sql.LoadData<aliasProfileModel, dynamic>("dbo.spProfileLookUp", p, "HandymanDB").First();

                /*Populating the lookup profile stored-procedure data*/
               
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }

            return tempProfile;
        }
       

        /*Posting a new Profile and a new Address Using a Transaction*/
        public void PostProfile(ProfileModel val)
        {
            

            using ( sql = new SQLDataAccess())
            {
                //Save the Address model
                sql.StartTransaction("HandymanDB");
                sql.SaveDataTransaction("dbo.spAddressInsert", new { StreetName = val.Address.StreetName, City = val.Address.City, PostalCode = val.Address.PostalCode, HouseNumber = val.Address.HouseNumber, Surburb = val.Address.Surburb });
                //

                try
                {
                    
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

        //Profile Update
        public void UpdateProfile(ProfileModel profile)
        {
            using (sql = new SQLDataAccess())
            {
                //Start transaction
                sql.StartTransaction("HandymanDB");
                                
                try
                {
                    //Update address
                    sql.SaveDataTransaction("dbo.spAddressUpdate", new { PostalCode = profile.Address.PostalCode, HouseNumber = profile.Address.HouseNumber, StreetName = profile.Address.StreetName, City = profile.Address.City, Surburb = profile.Address.Surburb, Id = profile.Address.Id });

                    //Update Profile
                    sql.SaveDataTransaction("dbo.spProfileUpdate", new { Name = profile.Name, Surname = profile.Surname, PhoneNumber = profile.PhoneNumber, DateOfBirth = profile.DateofBirth, Id = profile.Id });
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

        //This following method returns a list of addresses in the given city
        public List<ProviderAddress> GetAddressesByCity(string City)
        {
             sql = new SQLDataAccess();
           
            try
            {
                addresses = sql.LoadData<List<ProviderAddress>, dynamic>("dbo.spAddressLookUpByCity", new { City = City }, "HandymanDB").First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                sql.Dispose();
            }
            return addresses;

        }

        //Getting all Addresses of of the following postal code
        public List<ProviderAddress>  GetAddressesByPostalCode(int PostalCode)
        {
             sql = new SQLDataAccess();
           
            try
            {
                addresses = sql.LoadData<List<ProviderAddress>, dynamic>("dbo.spAddressLookUpByCity", new { PostalCode = PostalCode }, "HandymanDB").First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                sql.Dispose();
            }
            return addresses;
        }

        //This function returns a list of all address in the given surburb
        public List<ProviderAddress> GetAddressesBySurburb(string Surburb)
        {


            sql = new SQLDataAccess();
            try
            {
                addresses = sql.LoadData<List<ProviderAddress>, dynamic>("dbo.spAddressLookUpByCity", new { Surburb = Surburb }, "HandymanDB").First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                sql.Dispose();
            }
            return addresses;
        }
    }
}