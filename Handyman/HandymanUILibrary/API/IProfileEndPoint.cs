using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IProfileEndPoint
    {
          Task<ProfileModel> GetProfile();
          Task<ProfileModel> PostProfile(ProfileModel profile);

    }
}