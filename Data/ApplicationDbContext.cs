using E_Invitation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Ocassion> Ocassions { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Rank> ranks { get; set; }
        public DbSet<Enclosure> enclosures { get; set; }
        public DbSet<Vacancy> vacancies { get; set; }
        public DbSet<VacancyPlan> vacancyPlans { get; set; }
        public DbSet<VacancyPlanMirror> MirrorvacancyPlans { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AddGestList> addGestLists { get; set; }
        public DbSet<OcassionStatus> ocassionStatuses { get; set; }
        public DbSet<OcassionMapping> ocassionMappings { get; set; }
        public DbSet<ECard> ECards { get; set; }
    }
}
