using Microsoft.Extensions.DependencyInjection;
using TestPlatform.BLL.Services.Implementation;
using TestPlatform.BLL.Services.Interfaces;
using TestPlatform.DAL.Repositories.Implementation;
using TestPlatform.DAL.Repositories.Interfaces;

namespace TestPlatform.WEB.Dependency
{
    public static class DependencyRegistry
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IResultService, ResultService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IResultRepository, ResultRepository>();
        }
    }
}
