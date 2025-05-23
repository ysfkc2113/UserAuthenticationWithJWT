using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.AdminService
{
    public interface IClubService
    {//for admin
        
        Task<ClubDto> GetOneClubByIdAsync(int id,bool trackChanges);
        
        Task DeleteClubAsync(int id, bool trackChanges);
        Task<ClubDto> CreateClubAsync(ClubDtoForInsertion clubDtoForInsertion,bool trackchanges);
        Task<ClubDto> UpdateClubAsync(AdminClubDtoForUpdate clubDtoForUpdate,int id,bool trackChanges);

        Task UpdateClubMangerAsync(string userName);
        //user da da kullanıldı
        Task<(IEnumerable<ClubDto> clubDto, MetaData metaData)> GetAllClubsAsync(ClubParameters clubParameters,
          bool trackChanges);
    }
}
