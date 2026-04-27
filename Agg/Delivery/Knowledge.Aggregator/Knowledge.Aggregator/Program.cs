using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Net;
using System.Reflection;
using Rega.Qimah.PermessionGroup;

var builder = WebApplication.CreateBuilder(args);

try
{

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddGrpc();
    builder.Services.AddGrpcReflection();
    builder.Services.AddServices();
    builder.Services.AddRateLimitings(builder.Configuration);
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Any, 7149, o => o.Protocols = HttpProtocols.Http2);

        options.Listen(IPAddress.Any, 5555, o => o.Protocols = HttpProtocols.Http1);
    });
    builder.Host.UseSerilog((context, configuration) => configuration
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext().
            ReadFrom.Configuration(context.Configuration));

    builder.Services.AddSwaggerGen();

    builder.Services.AddChallengesGrpcClients(builder.Configuration["GrpcServiceSettings:ChallengesUrl"]);
    builder.Services.AddCPAggregatorGrpcClients(builder.Configuration["GrpcServiceSettings:CPAggregatorUrl"]);
    builder.Services.AddBlogGrpcClients(builder.Configuration["GrpcServiceSettings:BlogUrl"]);
    builder.Services.AddKnowLedgeGrpcClients(builder.Configuration["GrpcServiceSettings:KnowledgeUrl"]);
    builder.Services.AddIdeaBankGrpcClients(builder.Configuration["GrpcServiceSettings:IdeaBankUrl"]);
    builder.Services.AddAskExpertGrpcClients(builder.Configuration["GrpcServiceSettings:AskExpertUrl"]);
    builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagementUrl"]);

    builder.Services.AddTransient<GlobalExceptionMiddleware>();


    #region Swagger
    builder.Services.AddSwaggerGen(c =>
    {

        c.AddSecurityDefinition(
        "token",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization
        }
            );
        c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "token"
                            },
                        },
                        Array.Empty<string>()
                    }
        }
            );
    });
    #endregion
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCors",
            builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000")
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
            });
    });
    #region Identity Server
    builder.Services.AddAuthentication("Bearer")
      .AddJwtBearer("Bearer", options =>
      {
          // the name of your api resources   
          options.Audience = "UserManagementServer";
          /// identity server url                    
          options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
          options.RequireHttpsMetadata = false;
      });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ResourceOwnerPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "4EMicroServices"); // Require scope associated with Resource Owner Password token
        });

        options.AddPolicy("ClientCredentialsPolicy", policy =>
        {
            policy.AddAuthenticationSchemes("Bearer");
            policy.RequireClaim("scope", "CPAggregatorAPI"); // Require scope associated with Client Credentials token
        });
    });
    #endregion

    #region AutoMapper
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    #endregion
    builder.Services.AddHostedService<AddPermissionsMiddleware>();
    builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
    var app = builder.Build();

    app.MapGrpcService<ChallengeService>();
    app.MapGrpcService<BlogServerService>();
    app.MapGrpcService<QuestionServerService>();
    app.MapGrpcService<AggregatorService>();
    app.MapGrpcService<IdeaBankService>();
    /*app.MapGrpcService<IdeaBankCommentService>();*/

    //app.MapGrpcService<KnowledgeManagementService>();
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
        RequestPath = "/wwwroot"
    });


    // Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowCors");
    app.UseStaticFiles();
    app.UseRouting();

    //app.UseHttpsRedirection();

    app.UseAuthorization();
    app.MapControllers();

    app.UseSerilogRequestLogging();

    app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
    app.Run();

}
catch (Exception ex) when (

// https://github.com/dotnet/runtime/issues/60600
ex.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && ex.GetType().Name is not "HostAbortedException")
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    Log.Fatal(ex, "Unhandled exception");
}
