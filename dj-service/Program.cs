using dj_service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
    );

    options.AddPolicy("ProdCorsPolicy", builder => builder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("cdentertainment.azurewebsites.net", "*.cdentertainment.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Dependency injection
builder.Services.AddSingleton<IDbAccess, PostgresAccess>();

builder.Services.AddTransient<IServiceAccess, ServiceAccess>();
builder.Services.AddTransient<IServiceLogic, ServiceLogic>();
builder.Services.AddTransient<IAboutAccess, AboutAccess>();
builder.Services.AddTransient<IAboutLogic, AboutLogic>();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevCorsPolicy");
}
else 
{
    app.UseCors("ProdCorsPolicy");
}

app.UseHttpsRedirection();

app.Run();
