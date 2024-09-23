﻿namespace Domain
{
    public class DokumentEntity
    {
        public int Id { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupnaCena { get; set; }
        public int KomitentId { get; set; }
        public KomitentEntity? Komitent { get; set; }
        public List<StavkaDokumentaEntity>? Stavke { get; set; }
    }
}
