using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers
{

    public class WorkshopHelper : IWorkshopHelper
    {
        IWorkshopEndPoint _work;
        IMemberHelper _memberHelper;

        MemberModel? member;
        WorkshopModel? workshop;

        public WorkshopHelper(IWorkshopEndPoint Workshop, IMemberHelper memberH)
        {
            _work = Workshop;
            _memberHelper = memberH;
        }



        /// <summary>
        /// Get the workshop with the logged in employee
        /// </summary>
        /// <returns>WorkshopModel</returns>
        /// <exception cref="Exception">Null Reference</exception>
        async Task<WorkshopModel> IWorkshopHelper.GetWorkshop()
        {
            try
            {

                if (await Getmember() != null)
                {
                    if (member.employeeProfile.UserId != null)
                    {
                        workshop = await _work.GetWorkshop(member.WorkId);
                        if (workshop != null)
                        {
                            workshop.Employees.Add(member);
                        }
                    }


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
        /// Get WorkShops 
        /// </summary>
        /// <returns></returns>
        public async Task<WorkshopModel> GetWorkShop(string regNumber) => await _work.GetWorkShop(regNumber);
        /// <summary>
        /// Add a new member to the WorkShop
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AddMemberToWorkShop(MemberModel member)
        {
            if (member == null)
            {
                return false;
            }

            if (member.WorkId == 0)
            {
                return false;
            }

            try
            {
                return await _work.EmployMember(member);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create a new workshop
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Null reference</exception>

        async Task<WorkshopModel> IWorkshopHelper.CreateWorkshop(WorkshopModel work)
        {
            if (work != null)
            {
                try
                {
                    workshop = await _work.CreateNewWorkshop(await StampWorkshopUserAsync(work));
                    return workshop ?? work;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return null;
        }

        /// <summary>
        /// Stamp a new workshop with employee and member id
        /// </summary>
        /// <param name="newBiz"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Null reference</exception>
        public async Task<WorkshopModel>? StampWorkshopUserAsync(WorkshopModel? newBiz)
        {
            try
            {
                if (newBiz == null)
                {
                    return newBiz;
                }

                newBiz.registration.name = newBiz.Name;
                newBiz.address.add_country = "Sout Africa";
                var result = await _memberHelper.StampWorkshopUserAsync(newBiz);

                return result;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// Get the logged in member of the workshop
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<MemberModel>? Getmember()
        {
            try
            {
                if (member == null || member.member_profileId == null)
                {
                    member = await _memberHelper.GetMember();
                }
                return member;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }


    }
}
