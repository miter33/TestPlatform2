using System.Collections.Generic;
using TestPlatform.Common.Entities;

namespace TestPlatform.Common.ViewModels
{
    public class ResultParamViewModel
    {
        public Result Result { get; set; }
        public Test Test { get; set; }
        public List<string> UserAnswer { get; set; }
    }
}
