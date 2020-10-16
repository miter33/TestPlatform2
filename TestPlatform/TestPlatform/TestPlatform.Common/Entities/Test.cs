using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.Entities
{
    public class Test
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Range(10, 100, ErrorMessage = "Значение должно быть от 10 до 100")]
        public int Rate { get; set; }
        [Range(1, 60, ErrorMessage = "Значение должно быть от 1 до 60")]
        public int? Timer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Question> Question { get; set; }
        public List<Result> Result { get; set; }
    }
}
