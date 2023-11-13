using FM_MyStat.Core.AutoMappers;
using FM_MyStat.Core.AutoMappers.Student;
using FM_MyStat.Core.AutoMappers.Users;
using FM_MyStat.Core.Services;
using FM_MyStat.Core.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<AdministratorService>();
            services.AddTransient<StudentService>();
            services.AddTransient<TeacherService>();
            services.AddTransient<EmailService>();
            services.AddTransient<HomeworkService>();
        }
        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperAdminProfile));
            services.AddAutoMapper(typeof(AutoMapperStudentProfile));
            services.AddAutoMapper(typeof(AutoMapperTeacherProfile));
            services.AddAutoMapper(typeof(AutoMapperHomework));
        }
    }
}
