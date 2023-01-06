﻿using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers;

public interface IBusinessHelper
{
    Task<BusinessModel> GetBusiness();
    Task CreateBusiness(BusinessModel newBiz);
    Task<ServiceProviderModel> GetProvider();

}