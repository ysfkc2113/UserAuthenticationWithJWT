using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(
                new Event { Id = 1,ClubId=1 , Title = "Karagöz ve Hacivat", Price = 75 },
                new Event { Id = 2, ClubId = 2, Title = "Mesnevi", Price = 175 },
                new Event { Id = 3, ClubId = 1, Title = "Devlet", Price = 375 }
            );
        }
    }
}
