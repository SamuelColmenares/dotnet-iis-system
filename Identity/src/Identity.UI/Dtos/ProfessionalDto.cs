namespace Identity.UI.Dtos
{
    public class ProfessionalDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public List<AgendaEntryDto> Agenda { get; set; } = [];
    }
}
