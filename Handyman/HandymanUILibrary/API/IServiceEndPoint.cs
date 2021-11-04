﻿using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IServiceEndPoint
    {
        Task<List<ServiceModel>> GetServices();

    }
}