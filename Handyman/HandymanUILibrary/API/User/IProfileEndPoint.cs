using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.User
{
    public interface IProfileEndPoint
    {
        Task<ProfileModel> GetProfile(string id);
        Task<ProfileModel> GetProfileById(int id);
        Task<ProfileModel> PostProfile(ProfileModel profile);
        Task UpdateProfile(ProfileModel profile);

        //Task<List<ProviderAddress>> GetAddressesByCiy(string City);
        ////Returning a list of Addresses by Surburb
        //Task<List<ProviderAddress>> GetAddressesBySurburb(string Surburb);
        ////Getting a list of addresses in the given Postal Code
        //Task<List<ProviderAddress>> GetAddressesByPostalCode(int PostalCode);


    }
}