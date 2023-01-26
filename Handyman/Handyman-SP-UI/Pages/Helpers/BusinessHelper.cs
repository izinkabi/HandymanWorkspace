using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
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
                if (business == null)
                {
                    //provider = null;
                    if (provider == null)
                    {
                        GetProvider();
                    }
                    if (provider.employeeProfile != null)
                    {
                        business = await _business.GetLoggedInEmployee(provider.employeeId);

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
        /// Create a new business
        /// </summary>
        /// <param name="newBiz"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Null reference</exception>

        async Task IBusinessHelper.CreateBusiness(BusinessModel newBiz)
        {
            if (newBiz != null)
            {
                try
                {
                    await _business.CreateNewBusiness(StampBusinessUserAsync(newBiz).Result);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

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
                if (newBiz != null)

                    newBiz.registration.name = newBiz.Name;
                newBiz.address.add_country = "Sout Africa";

                return await _providerHelper.StampBusinessUserAsync(newBiz);
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
                if (provider == null)
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
