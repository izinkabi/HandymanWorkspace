using HandymanUILibrary.API.Services;
using HandymanUILibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Handymen_UI_Consumer.Pages;

public class IndexPageModel : PageModel
{


    private IServiceEndPoint _serviceEndPoint;

    private List<ServiceModel>? services;
    internal string? ErrorMsg;

    [BindProperty(SupportsGet = true)]
    public string? searchString { get; set; }
    public SelectList? serviceCategorySelectList { get; set; }

    public IndexPageModel(IServiceEndPoint serviceEndPoint)
    {
        _serviceEndPoint = serviceEndPoint;
    }

    //The only method runing on ServicesHome soon to be index
    public async Task OnGetAsync()
    {

        await LoadServices();


        if (!string.IsNullOrEmpty(searchString))
        {
            services = services.Where(s => s.name.ToLower()!.Contains(searchString.ToLower())).ToList();
        }

        //To enable the searchByCategory funtionality uncomment this below and same in HTML page
        //if (!string.IsNullOrEmpty(category))
        //{
        //    serviceDisplayList = serviceDisplayList.Where(x => x.CategoryName == category).ToList();
        //}
        //Populate the select list, could have been done so easily


    }

    //A local service list variable
    [BindProperty(SupportsGet = true)]
    public List<ServiceModel> ServiceList
    {
        get
        {
            return services;
        }
        set
        {
            services = value;
        }
    }

    //Load the services from UILibrary then populate
    async Task LoadServices()
    {


        try
        {
            //await the endpoint
            services = await _serviceEndPoint.GetServices();
            serviceCategorySelectList = new SelectList(services);
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }

    }


}
