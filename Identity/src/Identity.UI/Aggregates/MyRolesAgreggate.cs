using Microsoft.AspNetCore.Identity;

namespace Identity.UI.Aggregates;

public class MyRolesAgreggate: IdentityRole
{
    public bool IsActive { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? Description { get; set; }
}
