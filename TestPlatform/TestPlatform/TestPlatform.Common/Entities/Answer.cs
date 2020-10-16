using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public Question Question { get; set; }
    }
}
