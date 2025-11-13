namespace Syac.Patient.Domain.ValueObjects;

public record Document
{
    public DocumentType DocumentType { get; init; }
    public string DocumentNumber { get; init; }

    public Document(DocumentType documentType, string documentNumber)
    {
        DocumentNumber = ValidateDocumentNumber(documentType, documentNumber);
        DocumentType = documentType;
    }

    private static string ValidateDocumentNumber(DocumentType documentType, string documentNumber)
    {
        switch (documentType)
        {
            case var dt when dt == DocumentType.PersonalId ||
            dt == DocumentType.DriverLicense:
                if (documentNumber.Length < 5 || documentNumber.Length > 10 || !documentNumber.All(char.IsDigit))
                {
                    throw new ApplicationException("Personal ID must be into 5 and 10 digits.");
                }

                return documentNumber;

            case var dt when dt == DocumentType.NationalId:
                if (documentNumber.Length != 6 || !documentNumber.All(char.IsDigit))
                {
                    throw new ApplicationException("National ID must be exactly 10 digits.");
                }

                return documentNumber;

            case var dt when dt == DocumentType.Passport:
                if (documentNumber.Length < 6 || documentNumber.Length > 9 || !documentNumber.All(char.IsLetterOrDigit))
                {
                    throw new ApplicationException("Passport number must be between 6 and 9 alphanumeric characters.");
                }

                return documentNumber;
          
        }
        return documentNumber;
    }
}
