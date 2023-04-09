using OnlineGameShopApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;

    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("dd/MM/yyyy"));
}); 

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

app.UseAuthorization();

app.MapControllers();

app.Run();

/*
 {
  "OrderDate": "02.02.2221",
  "UserId": "0B97EF5A-DD3B-4167-BC4C-7B453FDA9108",
  "GameId": "09F863B7-8016-49DB-98CD-614A68331446"
}
 */