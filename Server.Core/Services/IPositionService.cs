using Server.Core.Entities;
using Server.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetPositionAsync();
    
        Task<Position> GetPositionByIdAsync(int id);

        Task<Position> AddPositionAsync(Position position);



    }
}
