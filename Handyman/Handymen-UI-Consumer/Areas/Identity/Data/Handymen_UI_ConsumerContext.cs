﻿using Handymen_UI_Consumer.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Handymen_UI_Consumer.Data;

public class Handymen_UI_ConsumerContext : IdentityDbContext<Handymen_UI_ConsumerUser>
{
    public Handymen_UI_ConsumerContext(DbContextOptions<Handymen_UI_ConsumerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<HandymanUILibrary.Models.ServiceModel>? Service { get; set; }

    public DbSet<HandymanUILibrary.Models.OrderModel>? Order { get; set; }
}
