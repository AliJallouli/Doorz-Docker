using System.Security.Claims;
using System.Text;
using Application.Logger;
using Application.UseCases.Auth.Service;
using Application.UseCases.Auth.UseCases.Authentication;
using Application.UseCases.Auth.UseCases.EmailConfirmation;
using Application.UseCases.Auth.UseCases.Password;
using Application.UseCases.Auth.UseCases.Register;
using Application.UseCases.Invitation.EntityAdmin.ColleagueInvitation.UseCase;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.UseCases;
using Application.UseCases.Legals.UseCases;
using Application.UseCases.References.EntityType.UseCase;
using Application.UseCases.References.Language.UseCase;
using Application.UseCases.Support.Services;
using Application.UseCases.Support.UseCases;
using Application.UseCases.UsersSite.UseCase;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Legals;
using Domain.Interfaces.Services;
using Domain.Interfaces.Support;
using Domain.Messaging.Interfaces;
using Infrastructure.Ef;
using Infrastructure.Ef.Data;
using Infrastructure.Ef.Interfaces;
using Infrastructure.Ef.Legals;
using Infrastructure.Ef.Support;
using Infrastructure.Messaging;
using Infrastructure.Options;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Configuration;
using WebApi.ExceptionHandlingMiddleware;
using WebApi.Middlewares;
using Mapper = Application.Mapper;


var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"
});

// Chargement ordonné de la configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


// AutoMapper
builder.Services.AddAutoMapper(typeof(Mapper));

// Dépendances
// Injection des repositories et use cases

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISuperRoleRepository, SuperRoleRepository>();
builder.Services.AddScoped<IInstitutionTypeRepository, InstitutionTypeRepository>();
// Register other services
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<LogoutUseCase>();
builder.Services.AddScoped<RegisterPublicUseCase>();
builder.Services.AddScoped<ConfirmEmaiUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISuperRoleRepository, SuperRoleRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<AddCompanyUseCase>();
builder.Services.AddScoped<AddInstitutionUseCase>();
builder.Services.AddScoped<RegisterAdminFromInviteUseCase>();
builder.Services.AddScoped<InviteColleagueUseCase>();
builder.Services.AddScoped<RegisterColleagueFromInviteUseCase>();
builder.Services.AddScoped<GetAllEntityTypesUseCase>();
builder.Services.AddScoped<GetAllInstitutionTypesUseCase>();
builder.Services.AddScoped<GetRolesByEntityTypeNameUseCase>();
builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<ValidateInvitationTokenUseCase>();
builder.Services.AddScoped<GetRoleIdByRoleNameANdENtityNameUseCase>();
builder.Services.AddScoped<ResendEmailConfirmationUseCase>();
builder.Services.AddScoped<SendPasswordResetLinkUseCase>();
builder.Services.AddScoped<ValidatePasswordResetTokenUseCase>();
builder.Services.AddScoped<ResetPasswordUseCase>();
builder.Services.AddScoped<ValidatePasswordResetOtpUseCase>();
builder.Services.AddScoped<ValidateTokenForEmailConfirmationUseCase>();
builder.Services.AddScoped<EmailConfirmedUseCase>();
builder.Services.AddScoped<ResendOtpCodeUseCase>();
builder.Services.AddScoped<ResendOtpCodeFromEmailUseCase>();
builder.Services.AddScoped<GetActiveLegalDocumentUseCase>();
builder.Services.AddScoped<GetAllLegalDocumentTypesUseCase>();
builder.Services.AddScoped<CreateContactMessageUseCase>();
builder.Services.AddScoped<GetAllSpokenLanguagesUseCase>();
builder.Services.AddScoped<GetAllContactMessageTypesByLangUseCase>();

builder.Services.AddScoped<ISuperadminInvitationRepository, SuperadminInvitationRepository>();
builder.Services.AddScoped<ISuperadminInvitationEntityRepository, SuperadminInvitationEntityRepository>();
builder.Services.AddScoped<IEntityTypeRepository, EntityTypeRepository>();
builder.Services.AddScoped<IEntityUserRepository, EntityUserRepository>();
builder.Services.AddScoped<ILandLordRepository, LandLordRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEntityRepository, EntityRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IInvitationTypeRepository, InvitationTypeRepository>();
builder.Services.AddScoped<IEmailAuthService, EmailAuthService>();
builder.Services.AddScoped<ITokenTypeRepository, TokenTypeRepository>();
builder.Services.AddScoped<ISecurityTokenRepository, SecurityTokenRepository>();
builder.Services.AddScoped<ISecurityTokenService, SecurityTokenService>();
builder.Services.AddScoped<ITokenHasher, TokenHasher>();
builder.Services.AddScoped<IInvitationEmailService, InvitationEmailService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IUserAgentRepository, UserAgentRepository>();
builder.Services.AddScoped<IOtpSendLogRepository, OtpSendLogRepository>();
builder.Services.AddScoped<ILegalConsentService, LegalConsentService>();
builder.Services.AddScoped<ILegalDocumentRepository, LegalDocumentRepository>();
builder.Services.AddScoped<IUserLegalConsentRepository, UserLegalConsentRepository>();
builder.Services.AddScoped<ISpokenLanguageRepository, SpokenLanguageRepository>();
builder.Services.AddScoped<ILegalDocumentTypeRepository, LegalDocumentTypeRepository>();
builder.Services.AddScoped<ILegalDocumentClauseTranslationRepository, LegalDocumentClauseTranslationRepository>();
builder.Services.AddScoped<ILegalDocumentTypeTranslationRepository, LegalDocumentTypeTranslationRepository>();
builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<IContactMessageTypeRepository, ContactMessageTypeRepository>();
builder.Services.AddScoped<IContactEmailService, ContactEmailService>();
builder.Services.AddScoped<RefreshToken>();
builder.Services.Configure<FrontendSettings>(builder.Configuration.GetSection("Frontend"));
builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("Emails"));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddHostedService<RabbitMqMailQueueConsumer>();
builder.Services.AddSingleton<IMailQueuePublisher<SendTemplatedEmailMessage>, RabbitMqMailQueuePublisher<SendTemplatedEmailMessage>>();





builder.Services.AddScoped<IEmailService>(sp => new EmailService(
    sp.GetRequiredService<IOptions<EmailSettings>>(),
    sp.GetRequiredService<IWebHostEnvironment>(),
    sp.GetRequiredService<ILogger<EmailService>>()
));


// Ajoute le DbContext (DoorsContext ou AppDbContext selon ton nom)
builder.Services.AddDbContext<DoorsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("db"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("db"))
    ));
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole(options => { options.FormatterName = "CustomFormatter"; });
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Debug);
    logging.Services.AddSingleton<ConsoleFormatter, CustomLoggerFormatter>();
    // Filtrer les logs du middleware par défaut pour éviter "unhandled exception" sur BusinessException
    logging.AddFilter("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogLevel.Warning);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOnly", policy =>
    {
        // Exige que l'utilisateur ait le rôle "SuperAdmin".
        policy.RequireRole("SuperAdmin");
    });
    options.AddPolicy("AdminOnly", policy =>
    {
        // Exige que l'utilisateur ait le rôle "Teacher".
        policy.RequireRole("Company.Admin", "Institution.Admin");
    });
});
//Jwt configuration starts here
//var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentNullException();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Lire le token depuis l'en-tête Authorization (Bearer)
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context => { return Task.CompletedTask; },
            OnTokenValidated = context => { return Task.CompletedTask; }
        };
    });

// Ajouter la configuration pour interpréter les en-têtes de proxy
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear(); // Accepter toutes les IPs (pour tests locaux)
    options.KnownProxies.Clear(); // Accepter tous les proxies (pour tests locaux)
    // En production, spécifiez les IPs de vos proxies connus, par exemple :
    // options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
});
//Jwt configuration ends here
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policyBuilder =>
    {
        var isLocalOrDocker = builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Docker";
        var origins = isLocalOrDocker
            ? new[] { "https://localhost:4200" }
            : new[] { "https://doorz.be", "https://www.doorz.be", "https://api.doorz.be" };

        policyBuilder
            .WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


builder.Services.AddLocalization(options => options.ResourcesPath = "");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "fr", "en", "nl", "de" };
    options.SetDefaultCulture("fr");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
});


// Ensure configuration is loaded
//var configuration = builder.Configuration;
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker" )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});


Console.WriteLine($"🌍 ENVIRONNEMENT ACTIF : {builder.Environment.EnvironmentName}");


app.UseForwardedHeaders();

app.UseCors("Default");
app.UseHttpsRedirection();
app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.Urls.Add("https://localhost:7200");
}
else if (app.Environment.IsProduction())
{
    app.Urls.Add("http://0.0.0.0:5000");
}


app.Run();