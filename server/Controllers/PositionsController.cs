using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Server.Core.DTOs;
using Server.Core.Entities;
using Server.Core.Services;
using Solid.Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("[controller]")]
    [ApiController]
   // [Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionsController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }


        // GET: <EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var positions = await _positionService.GetPositionAsync();
            return Ok(_mapper.Map<IEnumerable<PositionDto>>(positions));
        }

        // GET: <EmployeeController>/2

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PositionDto>(position));
        }
        // POST <EmployeeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PositionPostModel model)
        {
            var newPosition = await _positionService.AddPositionAsync(_mapper.Map<Position>(model));
            return Ok(_mapper.Map<PositionDto>(newPosition));
        }
    }
}
