using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Json;

namespace SP_MLibrary.Services.Implementation;

public class MemberEndpoint : EmployeeEndPoint, IMemberEndpoint
{

    private readonly IServiceHelper _ServiceHelper;
    private readonly AuthenticatedUserModel _authedUser;
    MemberModel Member;

    public MemberEndpoint(IServiceHelper _serviceHelper, AuthenticatedUserModel authedUserModel) : base(_serviceHelper)
    {
        _ServiceHelper = _serviceHelper;
        _authedUser = authedUserModel;
    }
    //Add Service(s) of business's member
    async Task IMemberEndpoint.AddService(MemberModel member)
    {
        try
        {
            await _ServiceHelper.ServicesClient.PostAsJsonAsync("/Services/Delivery/PostmemberService", member);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Remove the Service of a member
    async Task IMemberEndpoint.RemoveService(ServiceModel service, string memberId)
    {
        try
        {
            await _ServiceHelper.ServicesClient.DeleteAsync($"/Services/Delivery/Delete?Id={service.id}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    //Adding a new Service member
    async Task<bool> IMemberEndpoint.CreateMember(MemberModel member)
    {
        try
        {

            if (member != null)
            {

                if (member.member_profileId is not null)
                {
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    responseMessage = await _ServiceHelper.ServicesClient.PostAsJsonAsync("/Services/Delivery/AddNewMember", member);
                    return responseMessage.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            return false;
            throw new Exception(ex.Message);

        }
    }

    //Create a new profile
    async Task<bool> IMemberEndpoint.CreateProfile(ProfileModel newProfile)
    {
        try
        {
            if (newProfile is null)
            {
                return false;
            }

            if (_authedUser != null && _authedUser.Access_Token != null || newProfile.UserId != null)
            {
                if (newProfile.UserId != null && string.IsNullOrEmpty(_authedUser.Access_Token))
                {
                    _authedUser.Access_Token = await _ServiceHelper.AuthenticateUser(newProfile.UserId);
                }

                if (!string.IsNullOrEmpty(_authedUser.Access_Token))
                {
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    _ServiceHelper.InitializeClient(_authedUser.Access_Token);
                    responseMessage = await _ServiceHelper.ServicesClient.PostAsJsonAsync("/Services/auth/PostProfile", newProfile);
                    return responseMessage.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                return false;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

    //Query profile by Id
    async Task<ProfileModel> IMemberEndpoint.GetProfile(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                ProfileModel profile = await _ServiceHelper.ServicesClient.GetFromJsonAsync<ProfileModel>($"/Services/Handymen/GetProfile?userId={id}");
                if (profile != null) return profile;

            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
