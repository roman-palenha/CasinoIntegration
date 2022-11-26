using AutoMapper;
using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.DataAccessLayer.Entities;

namespace CasinoIntegration.BusinessLayer
{
    public class CasinoIntegrationMapperProfile: Profile
    {
        public CasinoIntegrationMapperProfile()
        {
            CreateMap<PlayerDTO, Player>().ReverseMap();
            CreateMap<MachineDTO, Machine>().ReverseMap();
        }
    }
}
