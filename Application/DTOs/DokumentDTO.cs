namespace Application.DTOs
{
    public class DokumentDTO
    {
        public string? BrojDokumenta { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime DatumDospeca { get; set; }
        public decimal UkupnaCena { get; set; }
        public KomitentDTO? Komitent { get; set; }
        public List<StavkaDokumentaDTO>? Stavke { get; set; }
    }
}
