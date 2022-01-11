var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    //test change
}

var bikeCompanies = new[]
{
    "Specialized","Giant","Canyon","Trek","Atherton","Cannondale","Fuji","Pivot","Evil"
};
var bikeModels = new[]
{ 
    "Enduro", "Stump Jumper", "Epic"
};

app.MapGet("/BikeBrands", () =>
{
    return bikeCompanies;    
})
.WithName("GetBikeBrands");

app.MapGet("/BikeModels", () =>
{
    return bikeModels;
})
.WithName("GetBikeModels");

app.Run();

