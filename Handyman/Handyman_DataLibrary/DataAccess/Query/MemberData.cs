using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class MemberData : EmployeeData, IMemberData
{
    ISQLDataAccess? _dataAccess;
    MemberModel? memberLocalmodel;
    public MemberData(ISQLDataAccess dataAccess) : base(dataAccess)
    {
        _dataAccess = dataAccess;
    }


    public List<MemberModel> GetMemberShips(int workId)
    {
        try
        {
            List<MemberModel> members = new List<MemberModel>();
            if (workId != 0)
            {

                //Get employee
                List<EmployeeModel> employees = GetMemberships(workId);


                //Get the service of the member (member's Service)
                foreach (var employee in employees)
                {
                    List<Service_CategoryModel> service_category = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Delivery.spMember_Service_LookUp",
                    new { memberId = employee.employeeId }, "Handyman_DB");


                    var member = new MemberModel()!;
                    //Membership
                    member.IsOwner = employee.IsOwner;
                    member.DateEmployed = employee.DateEmployed;
                    member.WorkshopId = employee.WorkshopId;

                    //Services
                    var services = new List<ServiceModel>();
                    foreach (Service_CategoryModel outputItem in service_category)
                    {
                        //populate service
                        var service = new ServiceModel()!;
                        service.datecreated = outputItem.serv_datecreated;
                        service.status = outputItem.serv_status;
                        service.img = outputItem.serv_img;
                        service.id = outputItem.serv_id;
                        service.name = outputItem.serv_name;

                        service.category = new ServiceCategoryModel();
                        //populate category
                        service.category.name = outputItem.cat_name;
                        service.category.description = outputItem.cat_description;
                        service.category.type = outputItem.cat_type;
                        services.Add(service);
                    }

                    member.Services = services;
                    members.Add(member);
                }


            }
            else
            {
                return null;
            }
            return members;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    //Since the service member is an employee, get the employee then
    //Get the service member and the services
    public MemberModel GetMember(string memberId)
    {
        try
        {
            if (memberId != null)
            {    //Get employee
                var employee = GetEmployeeWithRatings(memberId);


                //Get the service of the member
                List<Service_CategoryModel> service_category = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Delivery.spMember_Service_LookUp",
                    new { member_profileId = memberId }, "Handyman_DB");


                var member = new MemberModel()!;
                member.WorkshopId = employee.WorkshopId;
                member.employeeId = employee.employeeId;
                member.member_profileId = employee.employeeId;
                member.DateEmployed = employee.DateEmployed;
                member.employeeProfile = employee.employeeProfile;

                var services = new List<ServiceModel>();
                if (service_category.Count > 0)
                {

                    foreach (Service_CategoryModel outputItem in service_category)
                    {
                        //populate service
                        var service = new ServiceModel()!;
                        service.datecreated = outputItem.serv_datecreated;
                        service.status = outputItem.serv_status;
                        service.img = outputItem.serv_img;
                        service.id = outputItem.serv_id;
                        service.name = outputItem.serv_name;
                        service.category = new ServiceCategoryModel();
                        service.PriceId = outputItem.price_id;

                        //populate category
                        service.category.name = outputItem.cat_name;
                        service.category.description = outputItem.cat_description;
                        service.category.type = outputItem.cat_type;
                        services.Add(service);
                    }
                }


                member.Services = services;
                memberLocalmodel = member;
                return member;
            }

            return memberLocalmodel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Insert the employee followed by member 
    public void InsertMember(MemberModel member)
    {
        try
        {
            //insert the employee details 
            if (member.employeeId != null && member.employeeProfile.UserId != null)
            {
                InsertEmployee(member);
            }

            if (member.Services != null && member.Services.Count() > 0)
            {
                foreach (var service in member.Services)
                {
                    //Insert the member 
                    _dataAccess.SaveData("Delivery.spMemberInsert",
                        new
                        {
                            member.member_profileId,
                            ServiceId = service.id
                        },
                        "Handyman_DB");
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
