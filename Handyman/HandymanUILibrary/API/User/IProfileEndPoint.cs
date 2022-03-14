using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IProfileEndPoint
    {
          Task<ProfileModel> GetProfile(string id);
          Task<ProfileModel> GetProfileById(int id);
          Task<ProfileModel> PostProfile(ProfileModel profile);
          Task UpdateProfile(ProfileModel profile);

        Task<List<ProfileModel.AddressModel>> GetAddressesByCiy(string City);
        //Returning a list of Addresses by Surburb
        Task<List<ProfileModel.AddressModel>> GetAddressesBySurburb(string Surburb);
        //Getting a list of addresses in the given Postal Code
        Task<List<ProfileModel.AddressModel>> GetAddressesByPostalCode(int PostalCode);
       

    }
}