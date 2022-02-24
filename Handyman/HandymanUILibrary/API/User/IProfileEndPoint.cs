using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IProfileEndPoint
    {
          Task<ProfileModel> GetProfile(UserModel user);
          Task<ProfileModel> GetProfileById(int id);
          Task<ProfileModel> PostProfile(ProfileModel profile);
          Task UpdateProfile(ProfileModel profile);
    }
}