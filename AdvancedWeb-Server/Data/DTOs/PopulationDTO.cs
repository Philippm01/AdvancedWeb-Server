namespace AdvancedWeb_Server.Data.DTOs
{
    public class PopulationDTO
    {
        public required string Name { get; set; }
        public required int ID { get; set; }
        public required string Iso2 { get; set; }
        public required string Iso3 { get; set; }
        public required decimal Population { get; set; }
    }
}
