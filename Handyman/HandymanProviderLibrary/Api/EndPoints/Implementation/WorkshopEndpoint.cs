using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class WorkshopEndPoint : IWorkshopEndPoint
{
    IAPIHelper? _apiHelper;
    IMemberEndpoint? _member;
    WorkshopModel? Workshop;
    string? ErrorMsg;
    public WorkshopEndPoint(IAPIHelper apiHelper, IMemberEndpoint member)
    {
        _apiHelper = apiHelper;
        _member = member;
    }

    //Get the Workshop's employee(Member)
    public async Task<WorkshopModel> GetWorkshop(int busId)
    {
        try
        {
            if (busId > 0)
            {
                Workshop = new();
                if (busId != null)
                {
                    Workshop = await _apiHelper?.ApiClient?.GetFromJsonAsync<WorkshopModel>($"/api/Delivery/GetWorkshop?busId={busId}");
                }

            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Workshop;
    }

    //Create a new Workshop
    public async Task<WorkshopModel> CreateNewWorkshop(WorkshopModel Workshop)
    {
        try
        {
            if (Workshop != null)
            {
                var response = await _apiHelper.ApiClient.PostAsJsonAsync<WorkshopModel>("/api/Delivery/Create", Workshop);
                WorkshopModel newWorkshop;
                if (response.IsSuccessStatusCode)
                {
                    newWorkshop = response.Content.ReadFromJsonAsync<WorkshopModel>().Result;
                    return newWorkshop;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    //Add new services under the Workshop's provider
    public async Task<bool> EmployMember(MemberModel Member)
    {
        try
        {
            if (Member != null)
            {
                var result = await _member.CreateMember(Member);
                return result;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            Member = null;
            return false;
            throw new Exception(ex.Message);
        }
    }



    public async Task<WorkshopModel> GetWorkShop(string regNumber)
    {
        try
        {
            var workShop = await _apiHelper.ApiClient.GetFromJsonAsync<WorkshopModel>($"/api/Delivery/GetWorkShop?regNumber={regNumber}");
            return workShop;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<bool> InsertWorkShopService(int workShopRegId, int customServiceId)
    {
        if (workShopRegId == 0 || customServiceId == 0)
        {
            return false;
        }

        try
        {
            var result = await _apiHelper.ApiClient.PostAsJsonAsync($"/api/delivery/InsertWorkShopService?workshopRegId={workShopRegId}&&customServiceId={customServiceId}", new { });
            if (result.IsSuccessStatusCode)
            {
                return true;
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
}
