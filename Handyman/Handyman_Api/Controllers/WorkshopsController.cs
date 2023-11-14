using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/Delivery")]
[ApiController]
[Authorize(Roles = "Member")]
public class WorkshopsController : ControllerBase
{
    IWorkshopData? _workshopData;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public WorkshopsController(IWorkshopData workshop,
        SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _workshopData = workshop;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }
    MemberModel? memberModel;
    WorkshopModel? workshop;

    /// <summary>
    /// Get the workshop under which the employee(userId) which is a Provider is registered
    /// </summary>
    /// <param name="workId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("GetWorkshop")]
    public WorkshopModel? Get(int? workId)
    {

        try
        {
            if (workId != null && workId.Value != 0)
            {
                workshop = new()!;
                workshop = _workshopData.GetWorkshopById(workId.Value);
                return workshop;
            }

            return workshop;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Register A workshop
    /// </summary>
    /// <param name="workshop"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [Route("Create")]
    public async Task<WorkshopModel> CreateWorkshop(WorkshopModel workshop)
    {
        try
        {
            if (workshop != null)
            {
                var claim = signInManager.Context.User;
                //Get the user of the given claim
                var user = await signInManager.UserManager.GetUserAsync(claim);

                string[] roles = new string[] { "Owner", "Member" };
                //Executing the query
                WorkshopModel workshopM = _workshopData.CreateWorkshop(workshop);
                //Provider role assignment
                if (workshopM.Id != 0)
                {
                    foreach (string role in roles)
                    {
                        var Exists = await roleManager.RoleExistsAsync(role);
                        if (Exists)
                        {
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                        }
                        else
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                        }

                    }
                }

                if (workshopM != null) return workshopM;

            }

            return null;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }



    /// <summary>
    /// Employ a new member
    /// </summary>
    /// <param name="member"></param>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [Route("AddNewMember")]
    public async Task<IActionResult> AddNewMember(MemberModel member)
    {
        try
        {
            if (member != null && member.member_profileId != null)
            {
                if (string.IsNullOrEmpty(member.employeeId))
                {
                    member.employeeId = member.member_profileId;
                }

                if (!string.IsNullOrEmpty(member.employeeId))
                {
                    _workshopData.EmployMember(member);
                    string role = "Member";
                    //Executing the query
                    WorkshopModel workshopM = _workshopData.CreateWorkshop(workshop);
                    //Provider role assignment
                    if (workshopM.Id != 0)
                    {

                        var claim = signInManager.Context.User;
                        //Get the user of the given claim
                        var user = await signInManager.UserManager.GetUserAsync(claim);
                        var Exists = await roleManager.RoleExistsAsync(role);
                        if (Exists)
                        {
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                            return Ok();
                        }
                        else
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                            var result = await signInManager.UserManager.AddToRoleAsync(user, role);
                            return Ok();
                        }

                    }
                }
                else
                {
                    return BadRequest("Invalid Input");
                }
            }
            return Ok();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    ///Get the Provider / the Provider of the Workshop System
    ///It is an employee and has profile 
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns>MemberModel</returns>
    /// <exception cref="Exception"></exception>

    [HttpGet]
    [Route("GetMember")]
    public MemberModel GetMember(string employeeId)
    {
        try
        {
            if (employeeId != null)
            {
                memberModel = _workshopData.GetMember(employeeId);
                return memberModel;
            }
            return memberModel;
        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Post a Provider/Service Provider
    /// </summary>
    /// <param name="member"></param>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    [Route("PostMemberService")]
    public void PostMemberService(MemberModel member)
    {
        try
        {
            if (member != null)
                _workshopData.AddMemberServices(member);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }


    /// <summary>
    /// Get a workshop by Registration ID
    /// </summary>
    /// <param name="regNumber">registration ID</param>
    /// <returns>WorkShopMpdel</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("GetWorkShop")]
    public WorkshopModel Get(string regNumber)
    {
        try
        {
            var workshop = _workshopData.GetWorkShop(regNumber);
            return workshop;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Insert new workshop service
    [HttpPost]
    [Route("InsertWorkShopService")]
    public void Post(int workshopRegId, int customServiceId)
    {
        if (workshopRegId == 0 || workshopRegId == 0)
        {
            return;
        }

        try
        {
            _workshopData.InsertWorkShopService(workshopRegId, customServiceId);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
