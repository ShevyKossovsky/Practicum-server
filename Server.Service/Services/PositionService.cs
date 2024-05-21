using Server.Core.Entities;
using Server.Core.Repositories;
using Server.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Service.Services
{
    public class PositionService:IPositionService
    {

        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<Position> GetPositionByIdAsync(int id)

        {
            return await _positionRepository.GetPositionByIdAsync(id);
        }

        public async Task<IEnumerable<Position>> GetPositionAsync()
        {
            return await _positionRepository.GetPositionAsync();
        }

        public async Task<Position> AddPositionAsync(Position position)
        {
            return await _positionRepository.AddPositionAsync(position);
        }


    }
}
