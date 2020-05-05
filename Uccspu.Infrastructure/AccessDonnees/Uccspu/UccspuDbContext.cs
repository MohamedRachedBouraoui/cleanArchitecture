using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Uccspu.Affaires.Communs.Interfaces;
using Uccspu.Domaine.Communs;
using Uccspu.Domaine.Entites;

namespace Uccspu.AccessDonnees
{
    public class UccspuDbContext : DbContext
    {

        private readonly IUtilisateurEnCoursService _utilisateurEnCours;
        public UccspuDbContext(DbContextOptions<UccspuDbContext> options) : base(options) { }

        public UccspuDbContext(DbContextOptions<UccspuDbContext> options, IUtilisateurEnCoursService utilisateurEnCours) : base(options)
        {
            _utilisateurEnCours = utilisateurEnCours;
        }


        #region DB-SETS
        public DbSet<Episode> Episodes { get; set; } 
        #endregion

        private static DebugLoggerProvider debugLoggerProvider = new DebugLoggerProvider();

        LoggerFactory _myLoggerFactory = new LoggerFactory(new[] { debugLoggerProvider });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var now = DateTime.Now;
            foreach (var entree in ChangeTracker.Entries<Auditable>())
            {
                switch (entree.State)
                {
                    case EntityState.Added:
                        entree.Entity.CreePar = _utilisateurEnCours.IdUtilisateur;
                        entree.Entity.CreeLe = now;
                        break;
                    case EntityState.Modified:
                        entree.Entity.ModifiePar = _utilisateurEnCours.IdUtilisateur;
                        entree.Entity.ModifieLe = now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
