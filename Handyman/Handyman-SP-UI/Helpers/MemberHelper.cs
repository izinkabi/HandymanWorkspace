using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Handyman_SP_UI.Helpers;

public class MemberHelper : ProfileHelper, IMemberHelper
{
    IMemberEndpoint? _memberEndPoint;
    AuthenticationStateProvider? _AuthenticationStateProvider;
    MemberModel? memberModel;

    private readonly IServiceEndpoint _serviceEndpoint;
    private readonly IWorkshopEndPoint _workShopEndPoint;




    public MemberHelper(IMemberEndpoint memberEndpoint,
        AuthenticationStateProvider AuthenticationStateProvider,
          IServiceEndpoint serviceEndpoint, IWorkshopEndPoint workShopEndPoint)
        : base(memberEndpoint, AuthenticationStateProvider)
    {
        _memberEndPoint = memberEndpoint;
        _AuthenticationStateProvider = AuthenticationStateProvider;

        _serviceEndpoint = serviceEndpoint;
        _workShopEndPoint = workShopEndPoint;
    }

    //Create a member profie
    public Task<bool> RegisterProfile(ProfileModel profile) => CreateProfile(profile);

    //Get the profile of the Handyman/member 
    async Task<ProfileModel> IMemberHelper.GetMemberProfile() => await GetProfile();

    //Register a Handyman / Service member
    async void IMemberHelper.RegisterHandyman(MemberModel newHandyman)
    {
        if (newHandyman != null && newHandyman.employeeProfile != null)
        {
            userId = await GetUserId() ?? string.Empty;
            if (!string.IsNullOrEmpty(userId))
            {
                newHandyman.member_profileId = userId;
                newHandyman.employeeProfile.UserId = userId;
                await _memberEndPoint.CreateMember(newHandyman);
            }
        }
    }

    /// <summary>
    /// Add new service using the employee's ID hence the member's ID
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    async Task IMemberHelper.AddService(ServiceModel service)
    {
        try
        {
            MemberModel member = new();
            if (member.member_profileId == null)
            {
                await GetUserId();
                member = await GetMember();
                member.Services.Add(service);
            }
            List<int> newServicesIDs = new List<int>();
            //insert new services and 

            CustomServiceModel workshopService = new CustomServiceModel();
            workshopService.basePrice = 100;
            workshopService.originalServiceId = service.id;
            workshopService.description = service.category.description;
            workshopService.imageUrl = service.img;
            workshopService.title = service.name;
            service.Customs = new();

            if (service.Customs != null)
            {
                service.Customs.Add(workshopService);

                if (member.WorkId > 0)
                {
                    int customID = await _serviceEndpoint.CreateCustomService(service);
                    int regNumber = (await _workShopEndPoint.GetWorkshop(member.WorkId)).registration.Id;
                    var result = await _workShopEndPoint.InsertWorkShopService(regNumber, customID);
                    if (result)
                    {
                        await _memberEndPoint.AddService(member);
                    }
                }
                else
                {
                    return;
                }


            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
    /// <summary>
    /// Check if the service is being provided
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    async Task<bool> IMemberHelper.IsServiceProvided(ServiceModel service)
    {
        try
        {
            if (memberModel == null)
            {
                memberModel = await GetMember();
            }

            if (memberModel != null && memberModel.Services.Count > 0)
            {
                foreach (var serviceModel in memberModel.Services)
                {
                    if (serviceModel.id == service.id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Get the loggedIn employee if they have an ID
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<MemberModel> GetMember()
    {
        try
        {
            if (userId == null)
            {
                userId = await GetUserId();
            }

            if (userId != null)
            {
                memberModel = await GetMember();
                if (memberModel != null)
                {
                    if (memberModel.employeeProfile.UserId == null)
                    {
                        memberModel.employeeProfile = await GetProfile();
                    }
                }
                return memberModel;
            }


            return null;

        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex);
        }

    }
    /// <summary>
    /// Stamp the new Workshop with the owner's details / IDs
    /// </summary>
    /// <param name="work"></param>
    /// <returns></returns>
    [Authorize("Member")]
    async Task<WorkshopModel> IMemberHelper.StampWorkshopUserAsync(WorkshopModel? work)
    {


        try
        {
            MemberModel member = new MemberModel();
            member.employeeId = await GetUserId();
            member.employeeProfile = await GetProfile();
            member.member_profileId = member.employeeId;

            work.registration.workType = work.Type;

            work.Employees.Add(member);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
        work.date = DateTime.Now;
        //UserManager UI to return registered users


        return work;

    }

    /// <summary>
    /// Get the employee ID hence the member ID and stamping the Order
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    async Task<OrderModel> IMemberHelper.StampNewOrder(OrderModel newOrder)
    {
        if (userId == null)
        {
            userId = await GetUserId();
        }
        if (newOrder != null && userId != null)
        {
            newOrder.order_employeeid = userId;
        }
        return newOrder;

    }

    //Get the prices of the service
    async Task<PriceModel> IMemberHelper.GetServicePrice(int priceId)
    {
        if (priceId == 0)
        {
            return null;
        }
        try
        {
            var price = await _serviceEndpoint.GetPrice(priceId);
            return price;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return null;
        }
    }

    //Insert a custom-Service of the workshop
    async Task<bool> IMemberHelper.InsertCustomService(ServiceModel service)
    {
        if (service is null)
        { return false; }
        try
        {
            //Insert the custom service
            int newCustomServiceId = await _serviceEndpoint.CreateCustomService(service);
            if (newCustomServiceId != 0)
            {
                var owner = await GetMember();
                var workShop = await _workShopEndPoint.GetWorkshop(owner.WorkId);
                int bi = workShop.registration.Id;
                return await _workShopEndPoint.InsertWorkShopService(bi, newCustomServiceId);
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

    //Get workshop services of the given workshop-registration id
    async Task<List<CustomServiceModel>> IMemberHelper.GetWorkShopServices()
    {
        try
        {
            //Await the ower and worshop 
            var owner = await GetMember();
            if (owner == null)
            {
                return new List<CustomServiceModel>();
            }
            int wsregid = (await _workShopEndPoint.GetWorkshop(owner.WorkId)).registration.Id;
            if (wsregid == 0)
            {
                return null;
            }
            //get workshop services (custom services)
            List<CustomServiceModel> workshopservices = await _serviceEndpoint.GetWorkShopServices(wsregid);
            return workshopservices;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    //Update workshop service
    async Task<bool> IMemberHelper.UpdateWorkShopService(CustomServiceModel wsServices)
    {
        if (wsServices is null)
        {
            return false;
        }
        try
        {
            return await _serviceEndpoint.UpdateWorkShopService(wsServices);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Remove a workshop service
    async Task<bool> IMemberHelper.DeleteWorkShopService(int wsServiceId, int ogServiceId)
    {

        if (wsServiceId == 0 && ogServiceId == 0)
        {
            return false;
        }
        try
        {
            //Await the ower and worshop 
            var owner = await GetMember();
            int wsregid = (await _workShopEndPoint.GetWorkshop(owner.WorkId)).registration.Id;
            if (wsregid == 0)
            {
                return false;
            }
            //remove service by query
            bool IsWSServiceDeleted = await _serviceEndpoint.DeleteWorkShopService(wsServiceId, wsregid);
            if (IsWSServiceDeleted)
            {
                return await _serviceEndpoint.DeleteMemberService(ogServiceId, owner.member_profileId);
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
}
