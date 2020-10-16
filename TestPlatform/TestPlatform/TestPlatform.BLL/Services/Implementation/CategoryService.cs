using System.Linq;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.Common.Entities;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.BLL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repo)
        {
            repository = repo;
        }

        public IQueryable<Category> Categories => repository.Categories;

        public void AddCategory(Category category)
        {
            if(category != null)
            {
                repository.AddCategory(category);
            }
        }

        public void UpdateCategory(Category category)
        {
            if(category != null)
            {
                repository.UpdateCategory(category);
            }
        }

        public Category DeleteCategory(int id)
        {
            var category = Categories.FirstOrDefault(p => p.Id == id);

            if(category != null)
            {
                repository.DeleteCategory(category);
            }

            return category;
        }
    }
}
