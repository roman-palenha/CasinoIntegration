﻿using CasinoIntegration.BusinessLayer.CasinoIntegration.Services.Interfaces;
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
        /// <param name="id">the id of machine to be changed</param>
        /// <param name="newSize">the new size of slots</param>
        /// <returns>an Ok response</returns>
        [HttpPut]
        public async Task<IActionResult> ChangeMachine([FromBody] string id, int newSize)
        {
            await _machineService.ChangeSlotsSize(id, newSize);
            return Ok();
        }

        /// <summary>
        /// Action for creating machine
        /// </summary>
        /// <param name="slotsSize">the integer size of slots array</param>
        /// <returns>an Ok response</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMachine(int slotsSize)
        {
            await _machineService.Create(new Machine { SlotsSize = slotsSize });
            return Ok();
        }
    }
}
