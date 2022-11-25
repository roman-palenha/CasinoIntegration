using CasinoIntegration.BusinessLayer.CasinoInegration.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.CasinoIntegration.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CasinoIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;

        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _machineService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeMachine([FromBody] string id, int newSize)
        {
            try
            {
                await _machineService.changeMachineSlotsSize(id, newSize);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMachine(int slotsSize)
        {
            try
            {
                await _machineService.Create(new Machine { SlotsSize = slotsSize });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
