using Microsoft.EntityFrameworkCore;
using Tezo.Todo.Data;
using Tezo.Todo.Repository;
using Tezo.Todo.Repository.Interfaces;
using Tezo.Todo.Services;
using Tezo.Todo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<TodoAPIDbContext>(options => options.UseInMemoryDatabase("TODO"));

builder.Services.AddDbContext<TodoAPIDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("TodoApiConnectionString")));


// Scoped lifetime means that a new instance of the service will be created once per client request within the scope of that request.
// It ensures that the same instance of the service is used throughout the entire scope of a single request.

builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// <IUserServices, UserService> =>  It means that whenever an object of type IUserServices is requested from the dependency injection container,
// an instance of UserService will be provided.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// used to automatically register mappings defined in profiles within those assemblies, then allows for easy configuration 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// it will add middleware 
// Middleware are the software components that are added to the application pipeline to handle requests and responses 
// It is responsible for determining which controller and action methods should handle the request. 

app.UseAuthorization();

app.MapControllers();

app.Run();
