using Operations;
using Operations.Sales;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddScoped<SalesFactDashboardQuery>();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

//CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();
await new StartServices().StartServicesApp();

Console.WriteLine("Servicios Iniciados. Presione una tecla para continuar...");
Console.ReadKey();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.Run();

