using Identity.UI.Aggregates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.UI.DBContexts;

public class MyIdentityDBContext: IdentityDbContext<MyUserAggregate, MyRolesAgreggate, string>
{
    public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options) : base(options)
    {
        
    }
}
