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
                SifraKomitenta = komitentEntity.SifraKomitenta,
                Naziv = komitentEntity.Naziv,
                Adresa = komitentEntity.Adresa,
                Grad = komitentEntity.Grad
            };
        }
    }
}
