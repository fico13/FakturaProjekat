namespace Application.DTOs
{
    public class DokumentDTO
    {
        public int Id { get; set; }
        public string? BrojDokumenta { get; set; }
        public DateOnly DatumIzdavanja { get; set; }
        public DateOnly DatumDospeca { get; set; }
        public decimal UkupnaCena { get; set; }
        public KomitentDTO? Komitent { get; set; }
        public List<StavkaDokumentaDTO>? Stavke { get; set; }
    }
}
