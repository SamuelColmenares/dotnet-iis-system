using Identity.UI.Enums;

namespace Identity.UI.Dtos;

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public long DocumentNumber { get; set; }
    public DocumentTypeEnum DocumentType { get; set; }
    public  string ConfirmPassword { get; set; }
}
