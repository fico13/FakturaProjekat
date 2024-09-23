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
                Naziv = komitentDTO.Naziv ?? string.Empty,
                Adresa = komitentDTO.Adresa ?? string.Empty,
            };
        }

        public static KomitentDTO ToKomitentDTO(this KomitentEntity komitentEntity)
        {
            return new KomitentDTO
            {
                Naziv = komitentEntity.Naziv,
                Adresa = komitentEntity.Adresa
            };
        }
    }
}
