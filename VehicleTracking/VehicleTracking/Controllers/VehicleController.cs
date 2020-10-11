
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Enum;
using Service.Interface;

namespace VehicleTracking.Controllers
{
    /// <summary>
    /// Vehicle controller
    /// </summary>
    [Route("v1/Vehicle")]
    [ApiController]
    public class VehicleController : BaseApiController
    {
        private readonly IVehicleService _vehicleService;
        private readonly IVehiclePositonRecord _vehicleRecordService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleService"></param>
        /// <param name="vehicleRecordService"></param>
        public VehicleController(IVehicleService vehicleService, IVehiclePositonRecord vehicleRecordService)
        {
            _vehicleService = vehicleService;
            _vehicleRecordService = vehicleRecordService;
        }


        /// <summary>
        /// Vehicle register
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(VehicleRegisterResponseDto), 200)]
        public async Task<IActionResult> Post([FromBody]VehicleRegisterDto vehicle)
        {
            var result = await _vehicleService.CreateAsync(vehicle);
            return Ok(result);

        }

        /// <summary>
        /// Record vehicle position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(CommonCreatedResponseDto), 200)]
        public async Task<IActionResult> RecordPosition([FromBody]PositionRecordRequestDto position)
        {
            var result = await _vehicleRecordService.RecordPositionAsync(position);
            return Ok(result);

        }

        [Route("[action]")]
        [HttpGet()]
        [ProducesResponseType(typeof(List<PositionTransactionResponeDto>), 200)]
        public async Task<IActionResult> GetAllPosition([FromQuery]int deviseId, [FromQuery] PositionRequest param)
        {
            var result = await _vehicleRecordService.GetAllPositionAsync(deviseId, param);
            return Ok(result);
        }

        /// <summary>
        /// Get current vehicle position
        /// </summary>
        /// <param name="deviseId"></param>
        /// <returns></returns>  
        [Authorize(Roles = nameof(RoleEnum.ADMIN))]
        [HttpGet("{deviseId}")]
        [ProducesResponseType(typeof(List<PositionTransactionResponeDto>), 200)]
        public async Task<IActionResult> Get([FromRoute]int deviseId)
        {
            var result = await _vehicleRecordService.GetCurrentPositionByIdAsync(deviseId);
            return Ok(result);
        }


        /// <summary>
        /// Get position by period of time
        /// </summary>
        /// <param name="deviseId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [Authorize(Roles = nameof(RoleEnum.ADMIN))]
        [HttpGet("{deviseId}/{from}/{to}")]
        [ProducesResponseType(typeof(List<PositionTransactionResponeDto>), 200)]
        public async Task<IActionResult> Get([FromRoute]int deviseId, [FromRoute]DateTime from, [FromRoute]DateTime to)
        {
            var result = await _vehicleRecordService.GetPositionByPeriodOfTimeAsync(deviseId, from, to);
            return Ok(result);
        }
    }
}