namespace HandymanProviderLibrary.Models
{
    public interface IloggedInUserModel
    {
        DateTime CreateDate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
        string Username { get; set; }
        string UserRole { get; set; }

        void ResetUserModel();
    }
}