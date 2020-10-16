using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Name { get; set; }
        public List<Test> Test { get; set; }
    }
}
