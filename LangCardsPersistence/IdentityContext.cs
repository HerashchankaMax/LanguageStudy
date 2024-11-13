using LangCardsDomain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LangCardsPersistence;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
            .ToTable("LangServiceUsers")
            .Property(p=>p.Id)
            .HasColumnName("UserIdentifier");
        
        builder.Entity<ApplicationUser>()
            .Property(p=>p.FirstName)
            .HasColumnName("UserFirstName");
        
        builder.Entity<ApplicationUser>()
            .Property(p=>p.LastName)
            .HasColumnName("UserLastName");
    }
}