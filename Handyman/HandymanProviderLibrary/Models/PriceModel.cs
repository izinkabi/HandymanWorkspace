namespace HandymanProviderLibrary.Models;

public class PriceModel
{
    public int Id { get; set; }
    public float base_Price { get; set; }
    public float negotiated_price { get; set; }
    public float paid_price { get; set; }
}
