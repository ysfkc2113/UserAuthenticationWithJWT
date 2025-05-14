using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClubUserNotFoundException : NotFoundException
    {
        public ClubUserNotFoundException(int id)
            : base($"The event with id : {id} could not found.") { }
    }
}