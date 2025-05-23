using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UsersService
{
    public interface IClubServiceUsers
    {
        Task<(IEnumerable<ClubDto> clubDto, MetaData metaData)> GetAllClubsAsync(ClubParameters clubParameters,
         bool trackChanges);
    }
}
