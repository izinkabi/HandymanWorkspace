using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace Handyman_SP_UI.Pages.Helpers
{
    [Authorize(Roles = "ServiceProvider")]
    public class BusinessHelper : IBusinessHelper
    {
        IBusinessEndPoint _business;
        IProviderHelper _providerHelper;

        ServiceProviderModel? provider;
        BusinessModel? business;

        public BusinessHelper(IBusinessEndPoint business, IProviderHelper providerH)
        {
            _business = business;
            _providerHelper = providerH;
        }



        /// <summary>
        /// Get the business with the logged in employee
        /// </summary>
        /// <returns>BusinessModel</returns>
        /// <exception cref="Exception">Null Reference</exception>
        public async Task<BusinessModel>? GetBusiness()
        {
            try
            {

                if (await GetProvider() != null)
                {
                    if (provider.employeeProfile.UserId != null)
                    {
                        business = await _business.GetBusiness(provider.BusinessId);
                        if (business != null)
                        {
                            business.Employees.Add(provider);
                        }
                    }


                }

                return business;
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
        public async Task<BusinessModel> GetWorkShop(string regNumber) => await _business.GetWorkShop(regNumber);
        /// <summary>
        /// Add a new member to the WorkShop
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AddMemberToWorkShop(ServiceProviderModel member)
        {
            if (member == null)
            {
                return false;
            }

            if (member.BusinessId == 0)
            {
                return false;
            }

            try
            {
                return await _business.EmployMember(member);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create a new business
        /// </summary>
        /// <param name="newBiz"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Null reference</exception>

        async Task<BusinessModel> IBusinessHelper.CreateBusiness(BusinessModel newBiz)
        {
            if (newBiz != null)
            {
                try
                {
                    business = await _business.CreateNewBusiness(StampBusinessUserAsync(newBiz).Result);
                    return business ?? newBiz;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return null;
        }

        /// <summary>
        /// Stamp a new business with employee and provider id
        /// </summary>
        /// <param name="newBiz"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Null reference</exception>
        public async Task<BusinessModel>? StampBusinessUserAsync(BusinessModel? newBiz)
        {
            try
            {
                if (newBiz == null)
                {
                    return newBiz;
                }
                newBiz.registration.name = newBiz.Name;
                newBiz.address.add_country = "Sout Africa";
                var result = await _providerHelper.StampBusinessUserAsync(newBiz);

                return result;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        /// <summary>
        /// Get the logged in provider of the business
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ServiceProviderModel>? GetProvider()
        {
            try
            {
                if (provider == null || provider.pro_providerId == null)
                {
                    provider = await _providerHelper.GetProvider();
                }
                return provider;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }


    }
}
