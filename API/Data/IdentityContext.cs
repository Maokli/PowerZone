using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class IdentityContext : IdentityDbContext<Client>
{
    public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {
            
        }
    
   public DbSet<MembershipType> MembershipTypes { get; set; }
}
}