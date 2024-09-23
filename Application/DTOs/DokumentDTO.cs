namespace Application.DTOs
{
    public class DokumentDTO
    {
        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupnaCena { get; set; }
        public KomitentDTO? Komitent { get; set; }
        public List<StavkaDokumentaDTO>? Stavke { get; set; }
    }
}
