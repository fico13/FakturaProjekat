namespace Domain
{
    public class StavkaDokumentaEntity
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public decimal UkupnaCenaStavke { get; set; }
        public string? BrojDokumenta { get; set; }
        public DokumentEntity? Dokument { get; set; }
        public string? SifraRobe { get; set; }
        public RobaEntity? Roba { get; set; }
    }
}
