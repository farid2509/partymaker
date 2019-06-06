using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace PartyWeb.Models
{
    namespace DataAccessPostgreSqlProvider
    {
        // >dotnet ef migration add testMigration in AspNet5MultipleProject
        public class PartyDbContext : DbContext
        {
            public PartyDbContext()
            {

                Database.EnsureCreated();
            }

            public PartyDbContext(DbContextOptions<PartyDbContext> options) : base(options)
            {
            }

            public DbSet<DbParty> ManyParties { get; set; }
            //public DbSet<DbFlight> Flights { get; set; }
            public static string ConnectionString { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql(PartyDbContext.ConnectionString);

                base.OnConfiguring(optionsBuilder);
            }
        }

        public class DbParty
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            /// <summary>
            /// Организационные моменты
            /// </summary>
            public string TypeofEvent { get; set; }
            /// <summary>
            /// Тип мероприятия
            /// </summary>
            public string Distination { get; set; }
            /// <summary>
            /// Место проведения
            /// </summary>
            public string Services { get; set; }
            /// <summary>
            /// Услуги
            /// </summary>
            
            /// <summary>
            /// Работники
            /// </summary>
            public virtual Collection<DbWorkers> Workers { get; set; }
        }

        public class DbWorkers
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int PartyId { get; set; }
            [ForeignKey("PartyId")]
            public virtual DbParty Party { get; set; }
            /// <summary>
            /// Имя
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Должность
            /// </summary>
            public string Position { get; set; }
            /// <summary>
            /// Стаж
            /// </summary>
            public int Experience { get; set; }

            public override string ToString()
            {
                return $"Имя: {Name}, Должность: {Position}, Стаж: {Experience}";
            }
        }
    }
}
