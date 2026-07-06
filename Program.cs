using Azure.Identity;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

///////     step-1
///////////////////////////////////////////////////////////////////////////////////////////////////////
// Hey app - now for API - you need to make sure user is Azure AD authenticated
// ---------------- Azure AD Authentication ----------------
builder.Services.AddAuthentication("Bearer")
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();
///////////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIApp", policy =>
    {
        policy.WithOrigins("http://localhost:5228")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseCors("AllowUIApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
