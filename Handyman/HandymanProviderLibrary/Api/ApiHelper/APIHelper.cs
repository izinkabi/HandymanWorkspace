﻿using HandymanProviderLibrary.Models;
using HandymanProviderLibrary.Models.AuthModels;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.ApiHelper;

public class APIHelper : IAPIHelper
{
    //readonly IHttpClientFactory _clientFactory;
    private HttpClient _apiClient;
    private IloggedInUserModel _loggedInUserModel;
    private readonly IConfiguration _Configuration;

    public APIHelper(IConfiguration configuration)
    {
        _Configuration = configuration;
        InitializeClient();
        //_loggedInUserModel = loggedInUser;

    }

    public HttpClient ApiClient
    {
        get
        {
            return _apiClient;
        }
    }


    //We initialize the HTTP client and format the clients headings to pass the data as a json objet
    private void InitializeClient()
    {
        //string api = "https://localhost:7271/api/";

        _apiClient = new HttpClient();
        _apiClient.BaseAddress = new Uri(_Configuration["Api"]);
        _apiClient.DefaultRequestHeaders.Accept.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/*+json"));
    }

    //Overloading the initializeClient method to pass the token as a parameter
    public void InitializeClient(string token)
    {
        InitializeClient();
        _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
    /// <summary>
    ///authenticate the user by passing a username and password to the token endpoint and return a authenticate user with a token
    /// </summary>
    /// <returns>logged in user model</returns>
    public async Task<string> AuthenticateUser(string username, string password)
    {

        LoginModel? data = new LoginModel
        {
            Email = username,
            Password = password

        };

        using (HttpResponseMessage httpResponse = await _apiClient.PostAsJsonAsync<LoginModel>("Auth/login", data))
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                var token = await httpResponse.Content.ReadAsStringAsync();
                return token;
            }
            else
            {
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }

    }

    //Overloading AuthenticateUser method to take a string of a user ID
    public async Task<string> AuthenticateUser(string userId)
    {
        try
        {
            LoginModel? data = new LoginModel
            {
                Email = "string@mail.com",
                Password = "string",
                RememberMe = false,
                UserId = userId

            };
            InitializeClient();
            using (HttpResponseMessage httpResponse = await _apiClient.PostAsJsonAsync<LoginModel>("Auth/login", data))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var token = await httpResponse.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    return string.Empty;
                }
            };
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public async Task<IloggedInUserModel> GetLoggedInUserInfor(string Token)
    {
        _apiClient.DefaultRequestHeaders.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
        _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

        using (HttpResponseMessage httpResponseMessage = await _apiClient.GetAsync("Auth/userprofile"))
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                var result = await httpResponseMessage.Content.ReadFromJsonAsync<loggedInUserModel>();
                //_loggedInUserModel.Token = Token;
                //_loggedInUserModel.Id = result.Id;
                //_loggedInUserModel.Username = result.Username;
                //_loggedInUserModel.Email = result.Email;
                //_loggedInUserModel.CreateDate = result.CreateDate;
                //_loggedInUserModel.FirstName = result.FirstName;
                //_loggedInUserModel.LastName = result.LastName;
                ////acquiring roles from a model(not recommended, but HEy it wOrKs!)
                //_loggedInUserModel.UserRole = result.UserRole;
                return result;
            }
            else
            {
                throw new Exception(httpResponseMessage.ReasonPhrase);
            }
        }


    }

    //Log the user out of the system ***cheat!
    public async Task<bool> LogOutuser()
    {

        try
        {
            //_apiClient.DefaultRequestHeaders.Clear();
            var response = await _apiClient.PostAsJsonAsync("auth/logout", new { });
            return response.IsSuccessStatusCode;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


}
