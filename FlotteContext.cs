namespace FlotteVoiture;
using Microsoft.EntityFrameworkCore;

public class FlotteContext : DbContext
{
    public DbSet<Vehicule> Vehicules { get; set; }
    public DbSet<Chauffeur> Chauffeurs { get; set; }
    public DbSet<Trajet> Trajets { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    // Configuration de la chaîne de connexion
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=flotte.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurer l'héritage TPH (Table per Hierarchy) pour les véhicules
        modelBuilder.Entity<Vehicule>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Voiture>("Voiture")
            .HasValue<Camion>("Camion")
            .HasValue<Moto>("Moto");

        // Configurer les relations
        modelBuilder.Entity<Trajet>()
            .HasOne(t => t.Chauffeur)
            .WithMany(c => c.Trajets)
            .HasForeignKey(t => t.ChauffeurId);

        modelBuilder.Entity<Trajet>()
            .HasOne(t => t.Vehicule)
            .WithMany(v => v.Trajets)
            .HasForeignKey(t => t.VehiculeId);

        modelBuilder.Entity<Maintenance>()
            .HasOne(m => m.VehiculeConcerne)
            .WithMany()
            .HasForeignKey(m => m.VehiculeId);
    }
}