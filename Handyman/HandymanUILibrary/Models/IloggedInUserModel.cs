using System;

namespace HandymanUILibrary.Models
{
    public interface IloggedInUserModel
    {
        DateTime CreateDate { get; set; }
        string Email { get; set; }
        string Token { get; set; }
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        void ResetUserModel();
    }
}