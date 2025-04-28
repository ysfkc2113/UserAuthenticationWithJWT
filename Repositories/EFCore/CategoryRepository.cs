using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneCategory(Category category)=> Create(category);

        public void DeleteOneCategory(Category categor) => Delete(categor);
        public void UpdateOneCategor(Category category) => Update(category);

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            var allcategories= await FindAll(trackChanges).OrderBy(y=> y.Name).ToListAsync();
            return allcategories;
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
           return await FindByCondition(b => b.CategoryId.Equals(id), trackChanges)
           .SingleOrDefaultAsync();
        }


    }
}
