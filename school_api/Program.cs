using Microsoft.EntityFrameworkCore;
using AutoMapper;
using school_api.Services;
using school_api.Data;
using school_api.Repoistories;
using school_api.Repoistories.Base;
using school_api.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add context
builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );


// Register UnitOfWork and Repositories
builder.Services.AddScoped( typeof( IMainRepoistory<> ), typeof( MainRepoistory<> ) );
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
