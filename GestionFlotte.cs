using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FlotteVoiture
{
    public class GestionFlotte

    {   private FlotteContext db = new FlotteContext();

        private List<Vehicule> vehicules = new List<Vehicule>();
        private List<Chauffeur> chauffeurs = new List<Chauffeur>();
        private List<Trajet> trajets = new List<Trajet>();
        private List<Maintenance> maintenances = new List<Maintenance>();

        public void AjouterVehicule(Vehicule vehicule)
        {
            if (!db.Vehicules.Any(v => v.Immatriculation == vehicule.Immatriculation))
            {
                db.Vehicules.Add(vehicule);
                db.SaveChanges();

                vehicule.MaintenanceDue += (sender, e) =>
                {
                    Console.WriteLine($"Alerte : Le véhicule {vehicule.Immatriculation} nécessite une maintenance !");
                };
            }
            else
            {
                Console.WriteLine("Un véhicule avec cette immatriculation existe déjà.");
            }
        }

        public void ModifierVehicule(string immatriculation, string nouvelleMarque, string nouveauModele, string nouveauKilometrageInput)
        {
            var vehicule = db.Vehicules.FirstOrDefault(v => v.Immatriculation == immatriculation);
            if (vehicule != null)
            {
                if (!string.IsNullOrEmpty(nouvelleMarque))
                    vehicule.Marque = nouvelleMarque;
                if (!string.IsNullOrEmpty(nouveauModele))
                    vehicule.Modele = nouveauModele;
                if (!string.IsNullOrEmpty(nouveauKilometrageInput))
                {
                    if (int.TryParse(nouveauKilometrageInput, out int nouveauKilometrage))
                    {
                        vehicule.Kilometrage = nouveauKilometrage;
                    }
                    else
                    {
                        Console.WriteLine("Kilométrage invalide, valeur non modifiée.");
                    }
                }
                db.SaveChanges();
                Console.WriteLine("Véhicule modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Véhicule non trouvé.");
            }
        }


        public void SupprimerVehicule(string immatriculation)
        {
            var vehicule = db.Vehicules.Include(v => v.Trajets).FirstOrDefault(v => v.Immatriculation == immatriculation);
            if (vehicule != null)
            {
                if (db.Trajets.Any(t => t.VehiculeId == vehicule.VehiculeId))
                {
                    throw new SuppressionVehiculeAssigneException("Impossible de supprimer un véhicule assigné à un trajet actif.");
                }
                db.Vehicules.Remove(vehicule);
                db.SaveChanges();
                Console.WriteLine("Véhicule supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Véhicule non trouvé.");
            }
        }

        public void AjouterChauffeur(Chauffeur chauffeur)
        {
            if (!db.Chauffeurs.Any(c => c.Nom == chauffeur.Nom))
            {
                db.Chauffeurs.Add(chauffeur);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Un chauffeur avec ce nom existe déjà.");
            }
        }

        public void ModifierChauffeur(string nom, string nouveauPermis, int nouvelleAnciennete)
        {
            var chauffeur = db.Chauffeurs.FirstOrDefault(c => c.Nom == nom);
            if (chauffeur != null)
            {
                chauffeur.Permis = nouveauPermis;
                chauffeur.Anciennete = nouvelleAnciennete;
                db.SaveChanges();
                Console.WriteLine("Chauffeur modifié avec succès.");
            }
            else
            {
                Console.WriteLine("Chauffeur non trouvé.");
            }
        }

        public void SupprimerChauffeur(string nom)
        {
            var chauffeur = db.Chauffeurs.Include(c => c.Trajets).FirstOrDefault(c => c.Nom == nom);
            if (chauffeur != null)
            {
                if (chauffeur.Trajets.Any())
                {
                    Console.WriteLine("Impossible de supprimer un chauffeur assigné à un trajet actif.");
                }
                else
                {
                    db.Chauffeurs.Remove(chauffeur);
                    db.SaveChanges();
                    Console.WriteLine("Chauffeur supprimé avec succès.");
                }
            }
            else
            {
                Console.WriteLine("Chauffeur non trouvé.");
            }
        }

        public void AjouterTrajet(Trajet trajet)
        {
            if (!trajet.Vehicule.Disponible)
            {
                throw new VehiculeEnPanneException("Le véhicule n'est pas disponible.");
            }

            if (!VerifierPermis(trajet.Chauffeur, trajet.Vehicule))
            {
                throw new PermisInvalideException("Le chauffeur n'a pas le permis approprié.");
            }

            // Mise à jour du kilométrage du véhicule
            trajet.Vehicule.Kilometrage += (int)trajet.Distance;

            // Vérification de la maintenance
            trajet.Vehicule.VerifierMaintenance();

            // Ajouter le trajet à la base de données
            db.Trajets.Add(trajet);

            // Enregistrer les modifications dans la base de données
            db.SaveChanges();

            Console.WriteLine($"Trajet assigné à {trajet.Chauffeur.Nom} avec le véhicule {trajet.Vehicule.Immatriculation}");
        }

        // Exposer les listes de chauffeurs et véhicules
        public bool ADesChauffeurs => db.Chauffeurs.Any();
        public bool ADesVehicules => db.Vehicules.Any();

        private bool VerifierPermis(Chauffeur chauffeur, Vehicule vehicule)
        {
            // Logique de vérification du permis en fonction du type de véhicule
            if (vehicule is Voiture && chauffeur.Permis.Contains("B"))
                return true;
            if (vehicule is Camion && chauffeur.Permis.Contains("C"))
                return true;
            if (vehicule is Moto && chauffeur.Permis.Contains("A"))
                return true;
            return false;
        }

        public void AfficherVehicules()
        {
            var vehicules = db.Vehicules.ToList();
            foreach (var vehicule in vehicules)
            {
                vehicule.AfficherDetails();
            }
        }

        public void AfficherChauffeurs()
        {
            var chauffeurs = db.Chauffeurs.ToList();
            foreach (var chauffeur in chauffeurs)
            {
                chauffeur.AfficherDetails();
            }
        }
        public void AfficherTrajets()
        {
            var trajets = db.Trajets.Include(t => t.Chauffeur).Include(t => t.Vehicule).ToList();
            foreach (var trajet in trajets)
            {
                trajet.AfficherDetails();
            }
        }

        // Méthodes pour la maintenance
        public void EffectuerMaintenance(Vehicule vehicule, string typeMaintenance, decimal cout)
        {
            vehicule.EffectuerMaintenance();
            var maintenance = new Maintenance(DateTime.Now, typeMaintenance, cout, vehicule);
            db.Maintenances.Add(maintenance);
            db.SaveChanges();
        }

        public void AfficherMaintenances()
        {
            var maintenances = db.Maintenances.Include(m => m.VehiculeConcerne).ToList();
            foreach (var maintenance in maintenances)
            {
                maintenance.AfficherDetails();
            }
        }

        // Méthodes pour les statistiques
        public void AfficherStatistiques()
        {
            var totalKilometres = db.Trajets.Sum(t => t.Distance);
            Console.WriteLine($"Total des kilomètres parcourus: {totalKilometres} km");

            var chauffeurActifData = db.Trajets
                .GroupBy(t => t.ChauffeurId)
                .Select(g => new
                {
                    ChauffeurId = g.Key,
                    TotalDistance = g.Sum(t => t.Distance)
                })
                .OrderByDescending(g => g.TotalDistance)
                .FirstOrDefault();

            if (chauffeurActifData != null)
            {
                var chauffeur = db.Chauffeurs.Find(chauffeurActifData.ChauffeurId);
                Console.WriteLine($"Chauffeur le plus actif: {chauffeur.Nom} avec {chauffeurActifData.TotalDistance} km");
            }
            else
            {
                Console.WriteLine("Aucun trajet trouvé pour calculer les statistiques.");
            }
        }

        // Méthodes pour obtenir un véhicule ou un chauffeur
        public Vehicule ObtenirVehicule(string immatriculation)
        {
            return db.Vehicules.FirstOrDefault(v => v.Immatriculation == immatriculation);
        }

        public Chauffeur ObtenirChauffeur(string nom)
        {
            return db.Chauffeurs.FirstOrDefault(c => c.Nom == nom);
        }
    }
}

