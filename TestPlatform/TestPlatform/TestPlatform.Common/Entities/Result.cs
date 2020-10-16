using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public double Point { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string UserName { get; set; }
        public bool IsSuccess { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}