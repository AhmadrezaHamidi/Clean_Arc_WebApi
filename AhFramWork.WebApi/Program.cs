using AhFramWork.Persistances;
using AhFramWork.Persistances.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
//var ddd =  webbuilder
// Add services to the container.



builder.Services.AddPersistance(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//unitOfWork.DbContext.Database.GetAppliedMigrations();
//unitOfWork.DbContext.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
