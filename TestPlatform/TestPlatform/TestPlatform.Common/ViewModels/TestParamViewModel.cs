using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestPlatform.Common.Entities;

namespace TestPlatform.Common.ViewModels
{
    public class TestParamViewModel
    {
        public Test Test { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Range(1, int.MaxValue, ErrorMessage = "Вы ввели недопустимое значение поля")]
        public int CountQuestions { get; set; }
        public List<CountAnswerViewModel> CountAnswerViewModel { get; set; }
        public bool IsTimer { get; set; }
    }
}
