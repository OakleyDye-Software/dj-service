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
        .WithOrigins("cdentertainment.co", "*.cdentertainment.co", "secure-pandsy-3wy789swgtdj4om8ih03d5ok.herokudns.com", "*.secure-pandsy-3wy789swgtdj4om8ih03d5ok.herokudns.com")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Dependency injection
builder.Services.AddSingleton<IDbAccess, PostgresAccess>();

builder.Services.AddTransient<IServiceAccess, ServiceAccess>();
builder.Services.AddTransient<IServiceLogic, ServiceLogic>();
builder.Services.AddTransient<IAboutAccess, AboutAccess>();
builder.Services.AddTransient<IAboutLogic, AboutLogic>();
builder.Services.AddTransient<ICounterAccess, CounterAccess>();
builder.Services.AddTransient<ICounterLogic, CounterLogic>();
builder.Services.AddTransient<IEventAccess, EventAccess>();
builder.Services.AddTransient<IEventLogic, EventLogic>();
builder.Services.AddTransient<IPricePackageAccess, PricePackageAccess>();
builder.Services.AddTransient<IPricePackageLogic, PricePackageLogic>();
builder.Services.AddTransient<IFAQAccess, FAQAccess>();
builder.Services.AddTransient<IFAQLogic, FAQLogic>();
builder.Services.AddTransient<IEmailAccess, EmailAccess>();
builder.Services.AddTransient<ISubmissionAccess, SubmissionAccess>();
builder.Services.AddTransient<ISubmissionLogic, SubmissionLogic>();

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
