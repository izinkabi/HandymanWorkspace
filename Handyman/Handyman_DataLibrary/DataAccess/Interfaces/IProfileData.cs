using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IProfileData
    {
        ProfileModel GetProfile(string userId);
        void InsertProfile(ProfileModel profile);
    }
}