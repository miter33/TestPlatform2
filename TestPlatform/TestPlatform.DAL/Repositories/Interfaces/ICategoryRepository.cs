using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
