﻿namespace SP_MMobile.Model;

public class ServiceModel
{
    public int id { get; set; }
    public string name { get; set; }
    public string img { get; set; }
    public ServiceCategoryModel category { get; set; }
    public DateTime datecreated { get; set; }
    public string status { get; set; }
    public int PriceId { get; set; }
    public List<CustomServiceModel>? Customs { get; set; } = new List<CustomServiceModel>()!;
}
