namespace HandymanProviderLibrary.Models;

public class RegistrationModel
{
    public int Id { get; set; }
    public string? name { get; set; }
    public string? regNumber { get; set; }
    public string? taxNumber { get; set; }
    public int businessType { get; set; }
}