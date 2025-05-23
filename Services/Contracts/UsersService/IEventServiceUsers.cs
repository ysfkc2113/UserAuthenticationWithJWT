using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UsersService
{
    public interface IEventServiceUsers
    { 
        Task<(LinkResponse linkResponse, MetaData metaData)> GetAllEventsAsync(LinkParameters linkParameters,
            bool trackChanges);
        Task<EventDto> GetOneEventByIdAsync(int id, bool trackChanges);
    }
}
