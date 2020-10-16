using System.Linq;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Context;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.DAL.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TestDbContext context;

        public CategoryRepository(TestDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Category> Categories => context.Categories;

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
