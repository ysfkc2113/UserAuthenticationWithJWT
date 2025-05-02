using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClubNotFoundException : NotFoundException
    {
        public ClubNotFoundException(int id) : base($"The club with id : {id} could not found.")
        {
        }
    }
}
