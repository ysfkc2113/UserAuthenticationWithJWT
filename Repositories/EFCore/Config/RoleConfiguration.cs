﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
                , new IdentityRole
                {
                    Name = "Club Manager",
                    NormalizedName = "CLUBMANAGER"
                }, new IdentityRole
                {
                    Name = "Academician",
                    NormalizedName = "ACADEMICIAN"
                }
                , new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
                );
        }
    }
}
