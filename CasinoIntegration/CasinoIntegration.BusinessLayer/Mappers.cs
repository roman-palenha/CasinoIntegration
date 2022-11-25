using AutoMapper;
using CasinoIntegration.BusinessLayer.CasinoIntegrationDTO;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;

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
