using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Json;

namespace SP_MLibrary.Services.Implementation;

public class WorkshopEndPoint : IWorkshopEndPoint
{
    IServiceHelper _ServicesHelper;
    IMemberEndpoint _member;
    WorkshopModel Workshop;
    string ErrorMsg;
    public WorkshopEndPoint(IServiceHelper _servicesHelper, IMemberEndpoint member)
    {
        _ServicesHelper = _servicesHelper;
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
                    Workshop = await _ServicesHelper?.ServicesClient?.GetFromJsonAsync<WorkshopModel>($"/Services/Delivery/GetWorkshop?busId={busId}");
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
                var response = await _ServicesHelper.ServicesClient.PostAsJsonAsync("/Services/Delivery/Create", Workshop);
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
            var workShop = await _ServicesHelper.ServicesClient.GetFromJsonAsync<WorkshopModel>($"/Services/Delivery/GetWorkShop?regNumber={regNumber}");
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
            var result = await _ServicesHelper.ServicesClient.PostAsJsonAsync($"/Services/delivery/InsertWorkShopService?workshopRegId={workShopRegId}&&customServiceId={customServiceId}", new { });
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
