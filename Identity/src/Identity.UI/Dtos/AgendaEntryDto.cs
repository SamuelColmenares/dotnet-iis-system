namespace Identity.UI.Dtos
{
    public class AgendaEntryDto
    {
        public DateTime FechaInicio { get; set; } = DateTime.MinValue;
        public bool IsAvailable { get; set; } = false;
    }
}