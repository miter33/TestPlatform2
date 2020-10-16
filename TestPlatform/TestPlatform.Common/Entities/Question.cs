using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.Entities
{
    public class Question
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Name { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public bool IsOpenType { get; set; }
        public List<Answer> Answer { get; set; }
    }
}
