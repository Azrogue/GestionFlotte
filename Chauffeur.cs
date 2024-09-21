using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FlotteVoiture
{
    public class Chauffeur
    {

        [Key]
        public int ChauffeurId { get; set; } // Clé primaire générée automatiquement

        public string Nom { get; set; }
        public string Permis { get; set; }
        public int Anciennete { get; set; }
        public List<Vehicule> VehiculesAffectes { get; private set; }

        // Navigation properties
        public ICollection<Trajet> Trajets { get; set; } = new List<Trajet>();

        public Chauffeur(string nom, string permis, int anciennete)
        {
            Nom = nom;
            Permis = permis;
            Anciennete = anciennete;
            VehiculesAffectes = new List<Vehicule>();
        }

        public void AffecterVehicule(Vehicule vehicule)
        {
            if (!VehiculesAffectes.Contains(vehicule))
            {
                VehiculesAffectes.Add(vehicule);
                Console.WriteLine($"{Nom} a été affecté au véhicule {vehicule.Immatriculation}");
            }
        }

        public void AfficherDetails()
        {
            Console.WriteLine($"Chauffeur: {Nom}, Permis: {Permis}, Ancienneté: {Anciennete} ans");
        }
    }
}

