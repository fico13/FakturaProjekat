namespace Application.DTOs
{
    public class DokumentDTO
    {
        public int Id { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupnaCena { get; set; }
        public KomitentDTO? Komitent { get; set; }
        public List<StavkaDokumentaDTO>? Stavke { get; set; }
    }
}
