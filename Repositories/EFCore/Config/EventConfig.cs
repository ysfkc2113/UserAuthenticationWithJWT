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
                new Event { Id = 1,ClubId=1 , Title = "Quin Gambit  ", Description="Gleneksel medeniyet 5.Satranç Müsabakası", EventDate=DateTime.Now.AddDays(5), Location="medeniyet university", PublishedByUserName= "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", CreatedTime=DateTime.Now },
                new Event { Id = 2, ClubId = 4, Title = "Mesneviden Tiyatrolar", Description ="Tiyatro etkinlikleri buluşması", EventDate = DateTime.Now.AddDays(6), Location = "medeniyet university", PublishedByUserName = "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", CreatedTime = DateTime.Now },
                new Event { Id = 3, ClubId = 2, Title = "Futbolllll", Description ="Futbol Turnuvası Maç Kura Çekimleri", EventDate = DateTime.Now.AddDays(5), Location = "medeniyet university", PublishedByUserName = "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", CreatedTime = DateTime.Now },
                new Event { Id = 4, ClubId = 2, Title = "Futbol Turnuvası 1.Tur ", Description ="Futbol Turnuvası İlk Maçı", EventDate = DateTime.Now.AddDays(5), Location = "medeniyet university", PublishedByUserName = "841f3f0b-5f97-4fe0-954b-0eaa2ddd5fbd", CreatedTime = DateTime.Now }
            );
        }
    }
}
