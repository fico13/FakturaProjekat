using Application.DTOs;
using Domain;

namespace Application.Mappers
{
    public static class DokumentMapper
    {
        public static DokumentDTO ToDokumentDTO(this DokumentEntity dokumentEntity)
        {
            return new DokumentDTO
            {
                DatumIzdavanja = dokumentEntity.DatumIzdavanja,
                UkupnaCena = dokumentEntity.UkupnaCena,
                Komitent =
                dokumentEntity.Komitent!.ToKomitentDTO(),
                Stavke = dokumentEntity.Stavke!.Select(s => s.ToStavkaDTO()).ToList()
            };
        }

        public static DokumentEntity ToDokumentEntity(this DokumentDTO dokumentDTO)
        {
            return new DokumentEntity
            {
                DatumIzdavanja = dokumentDTO.DatumIzdavanja,
                UkupnaCena = dokumentDTO.UkupnaCena,
                Komitent = dokumentDTO.Komitent!.ToKomitentEntity(),
                Stavke = dokumentDTO.Stavke!.Select(s => s.ToStavkaDokumentaEntity()).ToList()
            };
        }
    }
}
