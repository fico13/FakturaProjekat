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
                CenaStavkeKom = stavkaDokumentaEntity.CenaStavkeKom,
                Kolicina = stavkaDokumentaEntity.Kolicina,
                UkupnaCenaStavke = stavkaDokumentaEntity.UkupnaCenaStavke,
                Roba = stavkaDokumentaEntity.Roba!.ToRobaDTO()
            };
        }

        public static StavkaDokumentaEntity ToStavkaDokumentaEntity(this StavkaDokumentaDTO stavkaDokumentaDTO)
        {
            return new StavkaDokumentaEntity
            {
                CenaStavkeKom = stavkaDokumentaDTO.CenaStavkeKom,
                Kolicina = stavkaDokumentaDTO.Kolicina,
                UkupnaCenaStavke = stavkaDokumentaDTO.UkupnaCenaStavke,
                Roba = stavkaDokumentaDTO.Roba!.ToRobaEntity()
            };
        }
    }
}
