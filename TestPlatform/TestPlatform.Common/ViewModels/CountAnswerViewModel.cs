using System.ComponentModel.DataAnnotations;

namespace TestPlatform.Common.ViewModels
{
    public class CountAnswerViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Range(1, 6, ErrorMessage = "Количество ответов должно быть от 1 до 6")]
        public int CountAnswers { get; set; }
    }
}
