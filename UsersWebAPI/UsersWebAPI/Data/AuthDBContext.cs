using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersWebAPI.Models.Domain;

namespace UsersWebAPI.Data
{
    public class AuthDBContext: IdentityDbContext<IdentityUser>
    {
        public AuthDBContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<User> Users {  get; set; }
    }
}
