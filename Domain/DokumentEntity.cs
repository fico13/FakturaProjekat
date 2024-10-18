namespace Domain
{
    public class DokumentEntity
    {
        public int Id { get; set; }
        public string? BrojDokumenta { get; set; }
        public DateOnly DatumIzdavanja { get; set; }
        public DateOnly DatumDospeca { get; set; }
        public decimal UkupnaCena { get; set; }
        public string? SifraKomitenta { get; set; }
        public KomitentEntity? Komitent { get; set; }
        public List<StavkaDokumentaEntity>? Stavke { get; set; }
    }
}
