namespace SP_MMobile.Model;

public class AddressModel
{

    public int Id { get; set; }
    public int add_Id { get; set; }
    public string? add_street { get; set; }
    public string? add_suburb { get; set; }
    public string? add_city { get; set; }
    public string? add_zip { get; set; }
    public float add_latitude { get; set; }
    public float add_longitude { get; set; }
    public string? add_country { get; set; }
    public string? add_state { get; set; }
}