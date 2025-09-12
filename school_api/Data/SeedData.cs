using school_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace school_api.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Seed Users
           
                var users = new List<User>
                {
                    new User
                    {
                        Name = "Admin User",
                        Email = "admin@school.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                        Role = "Admin",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Name = "Manager User",
                        Email = "manager@school.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Manager123!"),
                        Role = "Manager",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Name = "Teacher User",
                        Email = "teacher@school.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher123!"),
                        Role = "Teacher",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Name = "Student User",
                        Email = "student@school.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                        Role = "Student",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new User
                    {
                        Name = "Parent User",
                        Email = "parent@school.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Parent123!"),
                        Role = "Parent",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            

            // Seed Departments
            if (!await context.Departments.AnyAsync())
            {
                var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Role == "Admin");
                var managerUser = await context.Users.FirstOrDefaultAsync(u => u.Role == "Manager");

                var departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Computer Science",
                        ManagerId = managerUser?.Id ?? 1
                    },
                    new Department
                    {
                        Name = "Mathematics",
                        ManagerId = managerUser?.Id ?? 1
                    },
                    new Department
                    {
                        Name = "Physics",
                        ManagerId = managerUser?.Id ?? 1
                    }
                };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();
            }

            // Seed Subjects
            if (!await context.Subjects.AnyAsync())
            {
                var subjects = new List<Subject>
                {
                    new Subject
                    {
                        Name = "Programming",
                        Description = "Computer Programming"
                    },
                    new Subject
                    {
                        Name = "Database Systems",
                        Description = "Database Design and Management"
                    },
                    new Subject
                    {
                        Name = "Calculus",
                        Description = "Advanced Calculus"
                    },
                    new Subject
                    {
                        Name = "Linear Algebra",
                        Description = "Linear Algebra and Applications"
                    },
                    new Subject
                    {
                        Name = "Mechanics",
                        Description = "Classical Mechanics"
                    }
                };

                await context.Subjects.AddRangeAsync(subjects);
                await context.SaveChangesAsync();
            }

            // Seed Student Classes
            if (!await context.StudentClasses.AnyAsync())
            {
                var csDepartment = await context.Departments.FirstOrDefaultAsync(d => d.Name == "Computer Science");
                var mathDepartment = await context.Departments.FirstOrDefaultAsync(d => d.Name == "Mathematics");

                var studentClasses = new List<StudentClass>
                {
                    new StudentClass
                    {
                        Name = "CS-101",
                        DepartmentId = csDepartment?.Id ?? 1
                    },
                    new StudentClass
                    {
                        Name = "CS-102",
                        DepartmentId = csDepartment?.Id ?? 1
                    },
                    new StudentClass
                    {
                        Name = "MATH-101",
                        DepartmentId = mathDepartment?.Id ?? 2
                    }
                };

                await context.StudentClasses.AddRangeAsync(studentClasses);
                await context.SaveChangesAsync();
            }

            // Seed Teachers
            if (!await context.Teachers.AnyAsync())
            {
                var teacherUser = await context.Users.FirstOrDefaultAsync(u => u.Role == "Teacher");
                var csDepartment = await context.Departments.FirstOrDefaultAsync(d => d.Name == "Computer Science");
                var programmingSubject = await context.Subjects.FirstOrDefaultAsync(s => s.Name == "Programming");

                if (teacherUser != null && csDepartment != null && programmingSubject != null)
                {
                    var teacher = new Teacher
                    {
                        Name = "Dr. Ahmed Ali",
                        Email = "ahmed.ali@school.com",
                        PhoneNumber = "+1234567890",
                        UserId = teacherUser.Id,
                        DepartmentId = csDepartment.Id,
                        SubjectId = programmingSubject.Id
                    };

                    await context.Teachers.AddAsync(teacher);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Parents
            if (!await context.Parents.AnyAsync())
            {
                var parentUser = await context.Users.FirstOrDefaultAsync(u => u.Role == "Parent");

                if (parentUser != null)
                {
                    var parent = new Parent
                    {
                        Name = "Hassan Ali",
                        PhoneNumber = "+1234567892",
                        Address = "123 Main Street, City",
                        UserId = parentUser.Id
                    };

                    await context.Parents.AddAsync(parent);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Students
            if (!await context.Students.AnyAsync())
            {
                var studentUser = await context.Users.FirstOrDefaultAsync(u => u.Role == "Student");
                var parent = await context.Parents.FirstOrDefaultAsync();
                var csClass = await context.StudentClasses.FirstOrDefaultAsync(sc => sc.Name == "CS-101");

                if (studentUser != null && csClass != null)
                {
                    var student = new Student
                    {
                        Name = "Mohammed Hassan",
                        DateOfBirth = new DateTime(2005, 5, 15),
                        Email = "mohammed.hassan@school.com",
                        PhoneNumber = "+1234567891",
                        UserId = studentUser.Id,
                        ParentId = parent?.Id ?? 0,
                        StudentClassId = csClass.Id
                    };

                    await context.Students.AddAsync(student);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
