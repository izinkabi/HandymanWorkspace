using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class WorkshopData : IWorkshopData
    {
        ISQLDataAccess? _dataAccess;
        IMemberData? _member;
        Models.MemberModel employee;
        public WorkshopData(ISQLDataAccess dataAccess, IMemberData member)
        {
            _dataAccess = dataAccess;
            _member = member;

        }

        //Register a work return registration work
        int Register(RegistrationModel registration)
        {
            int registrationId = 0;
            try
            {
                registrationId = _dataAccess.LoadData<int, dynamic>("Delivery.spRegistrationInsert",
                    new
                    {

                        reg_name = registration.name,
                        reg_regnumber = registration.regNumber,
                        reg_taxnumber = registration.taxNumber,
                        reg_workType = registration.workType

                    }, "Handyman_DB").FirstOrDefault();
                return registrationId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //Get WorkShop Id
        public WorkshopModel GetWorkshopById(int? workshopId)
        {
            try
            {
                if (workshopId != null && workshopId.Value != 0)
                {

                    WorkshopRegistrationModel workshopRegModel = _dataAccess.LoadData<WorkshopRegistrationModel, dynamic>("Delivery.spWorkshop_Registration_LookUp",
                    new { workshopId = workshopId }, "Handyman_DB").FirstOrDefault();

                    //Populating work
                    WorkshopModel workshop = new()!;

                    workshop.Id = workshopRegModel.work_Id;
                    workshop.date = workshopRegModel.work_datecreated;
                    workshop.Type = workshopRegModel.reg_workType;
                    workshop.Name = workshopRegModel.work_name;
                    workshop.Description = workshopRegModel.work_description;

                    //Reistration

                    workshop.registration.Id = workshopRegModel.reg_Id;
                    workshop.registration.name = workshopRegModel.reg_name;
                    workshop.registration.regNumber = workshopRegModel.reg_regnumber;
                    workshop.registration.taxNumber = workshopRegModel.reg_taxnumber;

                    //Address
                    workshop.address.add_state = workshopRegModel.add_state;
                    workshop.address.add_zip = workshop.address.add_zip;
                    workshop.address.add_Id = workshopRegModel.add_Id;
                    workshop.address.add_country = workshopRegModel.add_country;
                    workshop.address.add_suburb = workshopRegModel.add_suburb;
                    workshop.address.add_street = workshopRegModel.add_street;

                    if (workshop != null) return workshop;

                }

            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }

        //Add the work-address and return the address *-can move to address data-*
        int RegisterAddress(AddressModel address)
        {
            try
            {
                //Insert the address and get an ID
                var add_id = _dataAccess.SaveData("Delivery.spAddressInsert",

                    new
                    {
                        add_street = address.add_street,
                        add_suburb = address.add_suburb,
                        add_city = address.add_city,
                        add_zip = address.add_zip,
                        add_latitude = address.add_latitude,
                        add_longitude = address.add_longitude,
                        add_country = address.add_country,
                        add_state = address.add_state
                    },
                    "Handyman_DB");
                return add_id;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //Posting a work and its address
        //Returns an ID of the newly created work
        public WorkshopModel CreateWorkshop(WorkshopModel workshop)
        {
            int workshopId = 0;
            try
            {

                if (workshop.registration != null && workshop.address != null)
                {
                    //Register and get an ID
                    int reg_id = Register(workshop.registration);

                    //Insert the address and get its id
                    int address_id = RegisterAddress(workshop.address);

                    _dataAccess.StartTransaction("Handyman_DB");
                    //Insert a new work and get an ID
                    workshopId = _dataAccess.LoadDataTransaction<int, dynamic>("Delivery.spWorkshopInsert",
                        new
                        {
                            //this is a new work (new address and registration)
                            workShop_registrationid = reg_id,
                            workShop_addressid = address_id,
                            workShop_datecreated = workshop.date,
                            workShop_name = workshop.Name,
                            workShop_description = workshop.Description

                        }).First();

                    _dataAccess.CommitTransation();
                    workshop.Id = workshopId;

                    //then save the employee
                    if (workshop.Employees != null && workshop.Employees.Count > 0)
                    {
                        foreach (var employee in workshop.Employees)
                        {
                            //Insert the employee
                            employee.WorkshopId = workshopId;
                            EmployMember(employee);
                        }

                    }


                    return workshop;
                }
            }
            catch (Exception ex)
            {
                _dataAccess.RollBackTransaction();
                throw new Exception(ex.Message);
            }
            return workshop;
        }

        //Create an employee in a work
        public void EmployMember(Models.MemberModel member)
        {
            try
            {
                //check if the Service member is a employee
                if (member is not null)
                {
                    //Insert a member and its employee details
                    _member?.InsertMember(member);

                    //Give the an employee a role 'Service member'

                    //_dataAccess.SaveData("spUserRoleInsert", new { userId = member.pro_memberId, roleName = "member" }, "Handyman_DB");
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        //Get the work by registration number
        public WorkshopModel GetWorkShop(string regNumber)
        {
            try
            {

                WorkshopRegistrationModel workshop = _dataAccess.LoadData<WorkshopRegistrationModel, dynamic>(
                    "Delivery.spWorkShopsLookUp",
                new { regNumber = regNumber }, "Handyman_DB").First();

                if (workshop != null)
                {
                    //Populating work
                    WorkshopModel work = new()!;

                    work.Id = workshop.work_Id;
                    work.date = workshop.work_datecreated;
                    work.Type = workshop.reg_workType;
                    work.Name = workshop.work_name;
                    work.Description = workshop.work_description;

                    //Reistration

                    work.registration.Id = workshop.reg_Id;
                    work.registration.name = workshop.work_name;
                    work.registration.regNumber = workshop.reg_regnumber;
                    work.registration.taxNumber = workshop.reg_taxnumber;

                    //Address
                    work.address.add_state = work.address.add_state;
                    work.address.add_zip = work.address.add_zip;
                    work.address.add_Id = work.address.add_Id;
                    work.address.add_country = work.address.add_country;
                    work.address.add_suburb = work.address.add_suburb;
                    work.address.add_street = work.address.add_street;


                    //Get employees for each work
                    work.Employees = _member.GetMemberShips(work.Id);

                    return work;

                }
                else
                {
                    return null;
                }


            }
            catch (Exception)
            {
                throw;
                return null;
            }

        }

        //Deleting the employee / Fire the nigga
        public void ReleaseEmployee()
        {
            //Also from _employeeData
        }

        //Get a Service member of the given employee ID
        public MemberModel GetMember(string employeeId)
        {
            try
            {
                MemberModel sp = _member?.GetMember(employeeId);
                return sp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void AddMemberServices(MemberModel member)
        {
            try
            {
                if (member != null && member.member_profileId != null)
                {
                    if (member.WorkshopId == 0)
                    {
                        return;
                    }
                    else
                    {
                        _member.InsertMember(member);
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        //Insert a new Service for the work
        public void InsertWorkShopService(int workShopRegId, int customServiceId)
        {
            if (workShopRegId == 0 || customServiceId == 0)
            {
                return;
            }
            try
            {
                _dataAccess.SaveData("Delivery.spWorkShopService_Insert", new { workShopRegId, customServiceId }, "Handyman_DB");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


    }
}
