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
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/Takes2Numbers", (int num1, int num2) => {
 return num1 + num2 + " is the sum of" + num1 + " and " + num2;
});

app.MapGet("/Morning", (string name, string time, string amOrpm) => {
 return "Hello " + name + " You woke up at " + time +amOrpm;
});

app.MapGet("/greaterOrLess", (int num1, int num2) => {
string result;
if(num1 > num2){
    result = num1 + " is greater than " + num2 + " and " + num2 + " is less than" + num1;
}else if(num1 < num2) {
    result = num1 + " is less than " + num2 + " and " + num1 + " is greater than " + num2;
}else {
    result = num1 + " is equal to " + num2 + " both numbers are equal";

}
return result;
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
