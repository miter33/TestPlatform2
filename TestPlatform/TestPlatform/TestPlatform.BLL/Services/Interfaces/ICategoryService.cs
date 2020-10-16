using System.Linq;
using TestPlatform.Common.Entities;

namespace TestPlatform.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        Category DeleteCategory(int id);
    }
}
