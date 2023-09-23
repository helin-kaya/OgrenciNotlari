using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OgrenciNotlariEL.Entities;
using OgrenciNotlariEL.IdentityModels;

namespace OgrenciNotlariDL.ContextInfo
{
    public class MyContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MyContext(DbContextOptions<MyContext> opt)
            : base(opt)
        {

        }
        public virtual DbSet<Lesson> LessonTable { get; set; }
        public virtual DbSet<Student> StudentTable { get; set; }
        public virtual DbSet<StudentLessonScore> StudentLessonScoreTable { get; set; }
        public virtual DbSet<UserForgotPasswordTokens> UserForgotPasswordTokensTable { get; set; }
        public virtual DbSet<UserForgotPasswordsHistorical> UserForgotPasswordsHistoricalTable { get; set; }
    }
}
