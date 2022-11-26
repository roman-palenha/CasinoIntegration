﻿using CasinoIntegration.BusinessLayer.DTO.Request;
using CasinoIntegration.BusinessLayer.Services.Interfaces;
using CasinoIntegration.DataAccessLayer.Entities;
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
            _machineService = machineService ?? throw new ArgumentNullException(nameof(machineService));
        }

        /// <summary>
        /// Action for getting all machines
        /// </summary>
        /// <returns>An enumerable of all machines</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _machineService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Action for changing machines size of slots array
        /// </summary>
        /// <param name="id">MachineId</param>
        /// <param name="machineSlotSize">Machine SlotSize</param>
        /// <returns>an Ok response</returns>
        [HttpPut]
        public async Task<IActionResult> ChangeMachine([FromQuery] string id, MachineSlotSizeDTO machineSlotSize)
        {
            await _machineService.ChangeSlotsSize(id, machineSlotSize);
            return Ok();
        }

        /// <summary>
        /// Action for creating machine
        /// </summary>
        /// <param name="machineDto">New machine to create</param>
        /// <returns>an Ok response</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMachine(MachineDTO machineDto)
        {
            await _machineService.Create(machineDto);
            return Ok();
        }
    }
}
