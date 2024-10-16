namespace Domain
{
    public class DokumentEntity
    {
        public int Id { get; set; }
        public string? BrojDokumenta { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime DatumDospeca { get; set; }
        public decimal UkupnaCena { get; set; }
        public string? SifraKomitenta { get; set; }
        public KomitentEntity? Komitent { get; set; }
        public List<StavkaDokumentaEntity>? Stavke { get; set; }
    }
}
