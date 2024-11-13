using Sail_MockApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//. DataServices here
builder.Services.AddScoped<ExampleDataService>();

builder.Services.AddSingleton<CheckinService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddSingleton<InformationService>();
builder.Services.AddSingleton<TimeblockService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<MapService>();   


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

// app.UseHttpsRedirection();


app.Run();
