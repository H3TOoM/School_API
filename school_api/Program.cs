using Microsoft.EntityFrameworkCore;
using AutoMapper;
using school_api.Services;
using school_api.Data;
using school_api.Repoistories;
using school_api.Repoistories.Base;
using school_api.Services.Base;
using school_api.Middleware;
using school_api.Middleware.ExceptionHandlers;
using school_api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add context
builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );


// Register UnitOfWork and Repositories
builder.Services.AddScoped( typeof( IMainRepoistory<> ), typeof( MainRepoistory<> ) );
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepoistory, AccountRepoistory>();


// add services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>(); 
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IAttendanceService , AttendanceService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IScheduleService ,  ScheduleService>();
builder.Services.AddScoped<IStaffService , StaffService>();
builder.Services.AddScoped<IStudentClassService , StudentClassService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuditService, AuditService>();

// Register exception handlers (Strategy pattern for OCP compliance)
builder.Services.AddSingleton<IExceptionHandler, BadRequestExceptionHandler>();
builder.Services.AddSingleton<IExceptionHandler, UnauthorizedExceptionHandler>();
builder.Services.AddSingleton<IExceptionHandler, NotFoundExceptionHandler>();
builder.Services.AddSingleton<IExceptionHandler, ConflictExceptionHandler>();
builder.Services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();

// Register sort strategies for search service (Strategy pattern for OCP compliance)
builder.Services.AddSingleton<ISortStrategy<Data.Models.Student>, Sorting.StudentSortStrategies.ByName>();
builder.Services.AddSingleton<ISortStrategy<Data.Models.Student>, Sorting.StudentSortStrategies.ByDateOfBirth>();
builder.Services.AddSingleton<ISortStrategy<Data.Models.Student>, Sorting.StudentSortStrategies.Default>();

// Register validators (SRP compliance - validation logic separated from controllers)
builder.Services.AddScoped<IValidator<DTOs.StudentCreateDto>, Validation.StudentCreateDtoValidator>();
builder.Services.AddScoped<IValidator<DTOs.StudentUpdateDto>, Validation.StudentUpdateDtoValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "School Management API", 
        Version = "v1",
        Description = "A comprehensive school management system API"
    });
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// JWT Authentication and Authorization using Options pattern
builder.Services.AddJwtAuthentication(builder.Configuration);

// Health Checks
builder.Services.AddHealthChecks();

// CORS using Options pattern
builder.Services.AddCorsConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

// Global Exception Handling
app.UseMiddleware<GlobalExceptionMiddleware>();

// CORS
app.UseCors("AllowSpecificOrigins");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health Check endpoint
app.MapHealthChecks("/health");


app.Run();
