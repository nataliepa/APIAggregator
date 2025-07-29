using System.Reflection;
using APIAggregator.Models.Dtos;
using APIAggregator.Services;
using APIAggregator.Services.Definitions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IDogService, DogService>();
builder.Services.AddHttpClient<IDogCeoImageService, DogCeoImageService>();
builder.Services.AddHttpClient<IWikipediaDogBreedInfoService, WikipediaDogBreedInfoService>();
builder.Services.AddHttpClient<ΙDogBreedExtraInfoService, DogBreedExtraInfoService>(
    client =>
    {
        client.BaseAddress = new Uri("https://api.thedogapi.com/v1/");
        client.DefaultRequestHeaders.Add("x-api-key", "live_qkouof8SEBMI97qYzxXowG3HAFwW2A0X6crOtTIgno0maEBfnfrtphR07MXtSYFR");        
    }
);

builder.Services.AddScoped<IDogAggregatorService, DogAggregatorService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
