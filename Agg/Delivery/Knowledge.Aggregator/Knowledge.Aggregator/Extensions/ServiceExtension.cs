using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace KnowledgeManagement.Aggregator.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionMiddleware, GlobalExceptionMiddleware>();
            services.AddScoped<IResponseHelper, ResponseHelper>();
			services.AddScoped<IChallengeService,ChallengeService>();
			services.AddScoped<ICommentService,CommentService>();
			services.AddScoped<IBlogService,BlogService>();
			services.AddScoped<IBlogCommentService,BlogCommentService>();
			services.AddScoped<IIdeaBankCommentService,IdeaBankCommentService>();
            services.AddScoped<ISubKnowledgeService,SubKnowledgeService>();
			services.AddScoped<IQuestionService,QuestionService>();
			services.AddScoped<IQuestionCommentService,QuestionCommentService>();
			services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
			services.AddScoped<IIdeaBankService, IdeaBankService>();
            services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
			services.AddSingleton<ITokenService,TokenService>();
			services.AddScoped<IKnowledgesService,KnowledgeService>();
			//services.AddSingleton<IKnowledgeManagementServices,KnowledgeManagementServices>();
		}

        public static void AddRateLimitings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                var rateLimitSection = configuration.GetSection("RateLimit");
                var permitLimit = rateLimitSection.GetValue<int>("PermitLimit");
                var queueLimit = rateLimitSection.GetValue<int>("QueueLimit");
                var windowSeconds = rateLimitSection.GetValue<int>("WindowSeconds");
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(windowSeconds);
                    opt.PermitLimit = permitLimit;
                    opt.QueueLimit = queueLimit;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });
        }
    }
}
