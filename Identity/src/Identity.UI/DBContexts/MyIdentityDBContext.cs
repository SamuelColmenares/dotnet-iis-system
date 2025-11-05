using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.UI.DBContexts;

public class MyIdentityDBContext: IdentityDbContext
{
    public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options) : base(options)
    {
        
    }
}
