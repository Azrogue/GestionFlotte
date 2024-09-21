using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlotteVoiture
{
    public class Trajet
    {
        [Key]
        public int TrajetId { get; set; } // Clé primaire générée automatiquement

        public string LieuDepart { get; set; }
        public string LieuArrivee { get; set; }
        public double Distance { get; set; }
        public TimeSpan Duree { get; set; }

        // Clés étrangères et navigation properties
        public int ChauffeurId { get; set; }
        public Chauffeur Chauffeur { get; set; }

        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }

        public Trajet(string lieuDepart, string lieuArrivee, double distance, TimeSpan duree, Chauffeur chauffeur, Vehicule vehicule)
        {
            LieuDepart = lieuDepart;
            LieuArrivee = lieuArrivee;
            Distance = distance;
            Duree = duree;
            Chauffeur = chauffeur;
            Vehicule = vehicule;
        }

        public void AfficherDetails()
        {
            Console.WriteLine($"Trajet de {LieuDepart} à {LieuArrivee}, Distance: {Distance} km, Durée: {Duree}");
            Console.WriteLine($"Chauffeur: {Chauffeur.Nom}, Véhicule: {Vehicule.Marque} {Vehicule.Modele}");
        }
        public Trajet() { } // Constructeur sans paramètre requis par EF Core
    }
}

