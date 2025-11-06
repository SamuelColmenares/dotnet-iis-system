using Identity.UI.Enums;
using Microsoft.AspNetCore.Identity;

namespace Identity.UI.Aggregates;

public class MyUserAggregate: IdentityUser
{
    public long DocumentNumber { get; set; }
    public DocumentTypeEnum DocumentType { get; set; }
}

