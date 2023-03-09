using ConnectionService.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IDbConnectionFactory, PostgresConnectionFactory>();
//builder.Services.AddHostedService<DatabaseController>().AddScoped<IDbSchemaProvider, PostgreSQLSchemaProvider>();
builder.Services.AddScoped<IDbSchemaProvider, PostgresSchemaProvider>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();