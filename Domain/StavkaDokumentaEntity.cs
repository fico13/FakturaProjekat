namespace Domain
{
    public class StavkaDokumentaEntity
    {
        public int Id { get; set; }
        public decimal CenaStavkeKom { get; set; }
        public int Kolicina { get; set; }
        public decimal UkupnaCenaStavke { get; set; }
        public int DokumentId { get; set; }
        public DokumentEntity? Dokument { get; set; }
        public int RobaId { get; set; }
        public RobaEntity? Roba { get; set; }
    }
}
