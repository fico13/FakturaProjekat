namespace Application.DTOs
{
    public class StavkaDokumentaDTO
    {
        public int Kolicina { get; set; }
        public decimal UkupnaCenaStavke { get; set; }
        public DokumentDTO? Dokument { get; set; }
        public RobaDTO? Roba { get; set; }
    }
}
