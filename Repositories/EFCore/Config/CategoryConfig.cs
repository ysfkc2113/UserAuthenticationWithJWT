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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(y=> y.CategoryId);
            builder.Property(y => y.Name).IsRequired();
            builder.HasData(
                new Category { CategoryId=1,Name="satranç kulübü"},
                new Category { CategoryId=2,Name="futbol kulübü"},
                new Category { CategoryId=3,Name="medeniyet tekno kulübü"});
        }
    }
}
