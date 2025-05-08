using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClubManager : IClubService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ClubManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _manager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ClubDto> GetOneClubByIdAsync(int id, bool trackChanges)
        {
            var club = await GetOneClubByIdAndCheckExists(id, trackChanges);
            var clubDto=_mapper.Map<ClubDto>(club);
            return clubDto;
           
        }
        public async Task<(IEnumerable<ClubDto> clubDto, MetaData metaData)> GetAllClubsAsync(ClubParameters clubParameters, bool trackChanges)
        {
            var eventsWithMetaData = await _manager
                .Club
                .GetAllClubsAsync(clubParameters, trackChanges);

            var clubDto = _mapper.Map<IEnumerable<ClubDto>>(eventsWithMetaData);


            return (clubDto, metaData: eventsWithMetaData.MetaData);

        }
        public async Task<ClubDto> CreateClubAsync(ClubDtoForInsertion clubDto, bool trackchanges)
        {
            var entity = _mapper.Map<Club>(clubDto);
            entity.CreatedTime = DateTime.Now;
            _manager.Club.CreateClub(entity);
            await _manager.SaveAsync();
            return _mapper.Map<ClubDto>(entity);
        }
        private async Task<Club> GetOneClubByIdAndCheckExists(int id, bool trackChanges)
        {

            // check entity 
            var entity = await _manager.Club.GetOneClubByIdAsync(id, trackChanges);

            if (entity is null)
                throw new ClubNotFoundException(id);

            return entity;
        }
        public async Task DeleteClubAsync(int id, bool trackChanges)
        {
            var entity = await GetOneClubByIdAndCheckExists(id, trackChanges);
            _manager.Club.DeleteClub(entity);
            await _manager.SaveAsync();
        }
        public async Task<ClubDto> UpdateClubAsync(AdminClubDtoForUpdate clubDtoForUpdate, int id, bool trackChanges)
        {
            var club= await GetOneClubByIdAndCheckExists(id,trackChanges);
            if(club.ClubId !=clubDtoForUpdate.ClubId)
            {
                throw new ClubNotFoundException(id);
            }
            _mapper.Map(clubDtoForUpdate, club);
            _manager.Club.UpdateClub(club);
            await _manager.SaveAsync();
            return _mapper.Map<ClubDto>(club);
        }
    }
}
