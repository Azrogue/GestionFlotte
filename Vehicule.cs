using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlotteVoiture
{
    public abstract class Vehicule : IMaintenable
    {
        [Key]
        public int VehiculeId { get; set; } // Clé primaire générée automatiquement

        public string Immatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public int Kilometrage { get; set; }
        public int KilometragePourMaintenance { get; set; }
        public bool Disponible { get; set; }
        public bool EnPanne { get; set; }

        // Pour la relation d'héritage avec EF Core
        public string? Discriminator { get; set; }

        // Propriété de navigation
        public ICollection<Trajet> Trajets { get; set; } = new List<Trajet>();

        public event EventHandler MaintenanceDue;

        public Vehicule(string immatriculation, string marque, string modele, int kilometragePourMaintenance, int kilometrageInitial = 0)
        {
            Immatriculation = immatriculation;
            Marque = marque;
            Modele = modele;
            Disponible = true;
            EnPanne = false;
            KilometragePourMaintenance = kilometragePourMaintenance;
            Kilometrage = kilometrageInitial;
        }

        public void EffectuerMaintenance()
        {
            Console.WriteLine($"{Marque} {Modele} ({Immatriculation}) a été entretenu.");
            Kilometrage = 0; // Réinitialise le kilométrage après maintenance
            EnPanne = false;
        }

        public void VerifierMaintenance()
        {
            if (Kilometrage >= KilometragePourMaintenance && MaintenanceDue != null)
            {
                MaintenanceDue(this, EventArgs.Empty);
            }
        }

        public abstract void AfficherDetails();
        public Vehicule() { } // Constructeur sans paramètre requis par EF Core
    }
}

