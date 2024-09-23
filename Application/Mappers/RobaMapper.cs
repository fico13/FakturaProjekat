using Application.DTOs;
using Domain;

namespace Application.Mappers
{
    public static class RobaMapper
    {
        public static RobaDTO ToRobaDTO(this RobaEntity robaEntity)
        {
            return new RobaDTO
            {
                Naziv = robaEntity.Naziv,
                Cena = robaEntity.Cena,
            };
        }

        public static RobaEntity ToRobaEntity(this RobaDTO robaDTO)
        {
            return new RobaEntity
            {
                Naziv = robaDTO.Naziv ?? string.Empty,
                Cena = robaDTO.Cena,
            };
        }
    }
}
