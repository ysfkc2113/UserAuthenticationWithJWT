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
    public class ClubConfig : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.HasKey(y=> y.ClubId);
            builder.Property(y => y.ClubName).IsRequired();
            builder.HasData(
                new Club { ClubId = 1, ClubName = "satranç kulübü", Description="Satranç meraklılarına." , Faculty="Elektronik",Created_at=DateTime.Now },
                new Club { ClubId = 2, ClubName = "futbol kulübü", Description = "Boş Gezenler İçin.", Faculty = "BESYO" ,Created_at = DateTime.Now },
                new Club { ClubId = 3, ClubName = "medeniyet tekno kulübü", Description = "Teknoloji Tutkunlarına.", Faculty = "Blgisayar Mühendisliği",Created_at = DateTime.Now });
        }
    }
}
