using HrManagerMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.DAL
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Holidays> Holidays { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Warnings> Warnings { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Todolist> Todolists { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<SendMessage> Messages { get; set; }

    }
}
