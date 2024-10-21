using Application.DTOs;
using Domain;

namespace Application.Mappers
{
    public static class StavkaDokumentaMapper
    {
        public static StavkaDokumentaDTO ToStavkaDTO(this StavkaDokumentaEntity stavkaDokumentaEntity)
        {
            return new StavkaDokumentaDTO
            {
                Id = stavkaDokumentaEntity.Id,
                Kolicina = stavkaDokumentaEntity.Kolicina,
                UkupnaCenaStavke = stavkaDokumentaEntity.UkupnaCenaStavke,
                Roba = stavkaDokumentaEntity.Roba!.ToRobaDTO(),
            };
        }

        public static StavkaDokumentaEntity ToStavkaDokumentaEntity(this StavkaDokumentaDTO stavkaDokumentaDTO)
        {
            return new StavkaDokumentaEntity
            {
                Id = stavkaDokumentaDTO.Id,
                SifraRobe = stavkaDokumentaDTO.Roba!.SifraRobe,
                Kolicina = stavkaDokumentaDTO.Kolicina,
                UkupnaCenaStavke = stavkaDokumentaDTO.UkupnaCenaStavke,
            };
        }
    }
}
