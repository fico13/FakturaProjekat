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
                Id = robaEntity.Id,
                SifraRobe = robaEntity.SifraRobe,
                Naziv = robaEntity.Naziv,
                JedinicaMere = robaEntity.JedinicaMere,
                Cena = robaEntity.Cena,
            };
        }

        public static RobaEntity ToRobaEntity(this RobaDTO robaDTO)
        {
            return new RobaEntity
            {
                Id = robaDTO.Id,
                SifraRobe = robaDTO.SifraRobe,
                Naziv = robaDTO.Naziv ?? string.Empty,
                Cena = robaDTO.Cena,
                JedinicaMere = robaDTO.JedinicaMere
            };
        }
    }
}
