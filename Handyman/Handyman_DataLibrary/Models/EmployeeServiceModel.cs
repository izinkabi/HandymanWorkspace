namespace Handyman_DataLibrary.Models
{
    public class EmployeeServiceModel
    {
        public string employeeId { get; set; }
        public IList<ServiceModel> myservices { get; set; }
    }
}
