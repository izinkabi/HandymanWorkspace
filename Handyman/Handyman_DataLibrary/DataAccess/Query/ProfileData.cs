using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class ProfileData : IProfileData
    {
        ISQLDataAccess _dataAccess;

        public ProfileData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void InsertProfile(ProfileModel profile)
        {
            if (profile != null)
            {
                try
                {
                    _dataAccess.SaveData("dbo.spProfileInsert", new
                    {
                        //Start with employee as employee' details profile 
                        Names = profile.Names,
                        Surname = profile.Surname,
                        DateOfBirth = profile.DateOfBirth.Date,
                        AddressId = profile.Address.add_Id,
                        PhoneNumber = profile.PhoneNumber,
                        EmailAddress = profile.EmailAddress,
                        profileGender = profile.Gender,
                        userId = profile.UserId,
                        ImageUrl = profile.ImageUrl



                    }, "Handyman_DB");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public ProfileModel GetProfile(string userId)
        {
            try
            {
                ProfileModel? profile = _dataAccess.LoadData<ProfileModel, dynamic>("spProfileLookUp", new { userId = userId }, "Handyman_DB").FirstOrDefault();
                return profile ?? null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
