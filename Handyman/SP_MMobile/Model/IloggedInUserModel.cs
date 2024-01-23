namespace SP_MMobile.Model;

public interface IloggedInUserModel
{
    string Token { get; set; }
    DateTime CreateDate { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string Id { get; set; }
    string LastName { get; set; }
    string Username { get; set; }
    string UserRole { get; set; }

    void ResetUserModel();
}