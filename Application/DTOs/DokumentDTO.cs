namespace Application.DTOs
{
    public class DokumentDTO
    {
        public string? BrojDokumenta { get; set; }
        public DateOnly DatumIzdavanja { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DatumDospeca { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public decimal UkupnaCena { get; set; }
        public KomitentDTO? Komitent { get; set; }
        public List<StavkaDokumentaDTO>? Stavke { get; set; }
    }
}
