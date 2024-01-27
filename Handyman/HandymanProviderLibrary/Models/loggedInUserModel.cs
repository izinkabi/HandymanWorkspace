namespace HandymanProviderLibrary.Models
{
    public class loggedInUserModel : IloggedInUserModel
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }

        public void ResetUserModel()
        {
            UserRole = "";
            Token = "";
            Id = "";
            Username = "";
            Email = "";
            CreateDate = DateTime.MinValue;

        }
    }
}
