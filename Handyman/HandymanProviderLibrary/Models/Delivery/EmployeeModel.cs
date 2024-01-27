namespace HandymanProviderLibrary.Models.Delivery
{
    public class EmployeeModel
    {
        public string? employeeId { get; set; }
        public IList<RatingsModel>? ratings { get; set; }
    }
}
