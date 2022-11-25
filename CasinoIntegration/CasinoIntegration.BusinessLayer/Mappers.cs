using AutoMapper;
using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.DataAccessLayer.Entities;

namespace CasinoIntegration.BusinessLayer
{
    public class Mappers: Profile
    {
        public Mappers()
        {
            CreateMap<PlayerDTO, Player>().ReverseMap();
        }
    }
}
