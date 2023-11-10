using api;
using api.Middleware;
using infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using service;
using service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataSource();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PasswordHashRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AccountService>();
builder.Services.AddSingleton<PostRepository>();
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<FollowRepository>();
builder.Services.AddSingleton<FollowService>();
builder.Services.AddJwtService();
builder.Services.AddSwaggerGenWithBearerJWT();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var frontEndRelativePath = builder.Environment.IsDevelopment() ? "./../frontend/www" : "../blog_frontend";
builder.Services.AddSpaStaticFiles(conf => conf.RootPath = frontEndRelativePath);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();
xU2VDGWoAhRZ89Imp4YSJbhnGvv5jf7vA5I278sFF4fb5JMwT2L2SC9UGfF/BlpnnixeUabYnwx3ahafVNzHuw==
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecurityHeaders();

app.UseSpaStaticFiles();
app.UseSpa(conf => { conf.Options.SourcePath = frontEndRelativePath; });

app.MapControllers();
app.UseMiddleware<JwtBearerHandler>();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();