using Application.DTOs;
using Domain;

namespace Application.Mappers
{
    public static class KomitentMapper
    {
        public static KomitentEntity ToKomitentEntity(this KomitentDTO komitentDTO)
        {
            return new KomitentEntity
            {
                Id = komitentDTO.Id,
                SifraKomitenta = komitentDTO.SifraKomitenta,
                Naziv = komitentDTO.Naziv,
                Adresa = komitentDTO.Adresa,
                Grad = komitentDTO.Grad
            };
        }

        public static KomitentDTO ToKomitentDTO(this KomitentEntity komitentEntity)
        {
            return new KomitentDTO
            {
                Id = komitentEntity.Id,
                SifraKomitenta = komitentEntity.SifraKomitenta,
                Naziv = komitentEntity.Naziv,
                Adresa = komitentEntity.Adresa,
                Grad = komitentEntity.Grad
            };
        }
    }
}
