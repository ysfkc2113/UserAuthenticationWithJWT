using AutoMapper;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UsersManagers
{
    public class ClubManagerUsers:IClubServiceUsers
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
      

        public ClubManagerUsers(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
       
        }
        public async Task<(IEnumerable<ClubDto> clubDto, MetaData metaData)> GetAllClubsAsync(ClubParameters clubParameters, bool trackChanges)
        {
            var eventsWithMetaData = await _manager
                .Club
                .GetAllClubsAsync(clubParameters, trackChanges);

            var clubDto = _mapper.Map<IEnumerable<ClubDto>>(eventsWithMetaData);


            return (clubDto, metaData: eventsWithMetaData.MetaData);

        }
    }
}
