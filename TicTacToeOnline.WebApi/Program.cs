using TicTacToeOnline.WebApi.Configuration;
using TicTacToeOnline.WebApi.Hubs;
using TicTacToeOnline.WebApi.Repositories;
using TicTacToeOnline.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var cosmosDbConfig = new CosmosDbConfig();
builder.Configuration.GetSection("CosmosDb").Bind(cosmosDbConfig);

// Registering dependecy injection
builder.Services.AddSingleton(cosmosDbConfig);
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddSingleton<IGameRepository, GameCosmosDBRepository>();

// Enabling CORS
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "angular-apps",
            policy =>
            {
                policy
                    .WithOrigins("https://thankful-plant-0bdca8e1e.2.azurestaticapps.net", "https://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<TicTacToeHub>("/gameHub");

app.UseCors("angular-apps");

app.Run();
