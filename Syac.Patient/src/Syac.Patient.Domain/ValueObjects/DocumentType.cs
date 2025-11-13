using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Patient.Domain.ValueObjects;

public record DocumentType
{
    public static readonly DocumentType Passport = new("Passport");
    public static readonly DocumentType NationalId = new("CedulaExtranjeria");
    public static readonly DocumentType DriverLicense = new("DriverLicense");
    public static readonly DocumentType PersonalId = new("CedulaCiudadania");

    public string Type { get; init; }

    private DocumentType(string type)
    {
        Type = type;
    }

    public static IReadOnlyCollection<DocumentType> List() =>
    [
        Passport,
        NationalId,
        DriverLicense,
        PersonalId
    ];

    public static DocumentType FromString(string type) =>
        List().SingleOrDefault(dt => dt.Type.Equals(type)) ?? 
        throw new ApplicationException("");

}
