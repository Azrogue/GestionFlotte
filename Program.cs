// Program.cs
using System;
using System.Linq;
using FlotteVoiture;

class Program
{
    static void Main(string[] args)
    {
        GestionFlotte flotte = new GestionFlotte();
        bool quitter = false;

        while (!quitter)
        {
            Console.Clear();
            Console.WriteLine("=== Gestion de Flotte de Véhicules ===");
            Console.WriteLine("1. Gestion des véhicules");
            Console.WriteLine("2. Gestion des chauffeurs");
            Console.WriteLine("3. Gestion des trajets");
            Console.WriteLine("4. Maintenance des véhicules");
            Console.WriteLine("5. Afficher les statistiques");
            Console.WriteLine("6. Quitter");
            Console.Write("Veuillez choisir une option : ");

            string choixPrincipal = Console.ReadLine();

            switch (choixPrincipal)
            {
                case "1":
                    MenuVehicules(flotte);
                    break;
                case "2":
                    MenuChauffeurs(flotte);
                    break;
                case "3":
                    MenuTrajets(flotte);
                    break;
                case "4":
                    MenuMaintenance(flotte);
                    break;
                case "5":
                    flotte.AfficherStatistiques();
                    Pause();
                    break;
                case "6":
                    quitter = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    Pause();
                    break;
            }
        }
    }

    // Menu pour la gestion des véhicules
    static void MenuVehicules(GestionFlotte flotte)
    {
        bool retour = false;
        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("=== Gestion des Véhicules ===");
            Console.WriteLine("1. Ajouter un véhicule");
            Console.WriteLine("2. Modifier un véhicule");
            Console.WriteLine("3. Supprimer un véhicule");
            Console.WriteLine("4. Afficher tous les véhicules");
            Console.WriteLine("5. Retour au menu principal");
            Console.Write("Veuillez choisir une option : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AjouterVehicule(flotte);
                    break;
                case "2":
                    ModifierVehicule(flotte);
                    break;
                case "3":
                    SupprimerVehicule(flotte);
                    break;
                case "4":
                    flotte.AfficherVehicules();
                    Pause();
                    break;
                case "5":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    Pause();
                    break;
            }
        }
    }

    // Program.cs

    static void AjouterVehicule(GestionFlotte flotte)
    {
        Console.Write("Type de véhicule (Voiture, Camion, Moto) : ");
        string type = Console.ReadLine();
        Console.Write("Immatriculation : ");
        string immat = Console.ReadLine();
        Console.Write("Marque : ");
        string marque = Console.ReadLine();
        Console.Write("Modèle : ");
        string modele = Console.ReadLine();
        Console.Write("Kilométrage initial : ");
        int kilometrageInitial;
        int.TryParse(Console.ReadLine(), out kilometrageInitial);

        Vehicule vehicule = null;

        switch (type.ToLower())
        {
            case "voiture":
                vehicule = new Voiture(immat, marque, modele, kilometrageInitial);
                break;
            case "camion":
                vehicule = new Camion(immat, marque, modele, kilometrageInitial);
                break;
            case "moto":
                vehicule = new Moto(immat, marque, modele, kilometrageInitial);
                break;
            default:
                Console.WriteLine("Type de véhicule invalide.");
                Pause();
                return;
        }

        flotte.AjouterVehicule(vehicule);
        Console.WriteLine("Véhicule ajouté avec succès.");
        Pause();
    }


    static void ModifierVehicule(GestionFlotte flotte)
    {
        Console.Write("Immatriculation du véhicule à modifier : ");
        string immat = Console.ReadLine();
        Console.Write("Nouvelle marque (laisser vide pour conserver) : ");
        string marque = Console.ReadLine();
        Console.Write("Nouveau modèle (laisser vide pour conserver) : ");
        string modele = Console.ReadLine();
        Console.Write("Nouveau kilométrage (laisser vide pour conserver) : ");
        string kilometrageInput = Console.ReadLine();

        flotte.ModifierVehicule(immat, marque, modele, kilometrageInput);
        Pause();
    }

    static void SupprimerVehicule(GestionFlotte flotte)
    {
        Console.Write("Immatriculation du véhicule à supprimer : ");
        string immat = Console.ReadLine();

        try
        {
            flotte.SupprimerVehicule(immat);
        }
        catch (SuppressionVehiculeAssigneException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Pause();
    }

    // Menu pour la gestion des chauffeurs
    static void MenuChauffeurs(GestionFlotte flotte)
    {
        bool retour = false;
        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("=== Gestion des Chauffeurs ===");
            Console.WriteLine("1. Ajouter un chauffeur");
            Console.WriteLine("2. Modifier un chauffeur");
            Console.WriteLine("3. Supprimer un chauffeur");
            Console.WriteLine("4. Afficher tous les chauffeurs");
            Console.WriteLine("5. Retour au menu principal");
            Console.Write("Veuillez choisir une option : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AjouterChauffeur(flotte);
                    break;
                case "2":
                    ModifierChauffeur(flotte);
                    break;
                case "3":
                    SupprimerChauffeur(flotte);
                    break;
                case "4":
                    flotte.AfficherChauffeurs();
                    Pause();
                    break;
                case "5":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    Pause();
                    break;
            }
        }
    }

    static void AjouterChauffeur(GestionFlotte flotte)
    {
        Console.Write("Nom : ");
        string nom = Console.ReadLine();
        Console.Write("Type de permis (A, B, C) : ");
        string permis = Console.ReadLine();
        Console.Write("Ancienneté (en années) : ");
        int anciennete;
        int.TryParse(Console.ReadLine(), out anciennete);

        Chauffeur chauffeur = new Chauffeur(nom, permis, anciennete);
        flotte.AjouterChauffeur(chauffeur);
        Console.WriteLine("Chauffeur ajouté avec succès.");
        Pause();
    }

    static void ModifierChauffeur(GestionFlotte flotte)
    {
        Console.Write("Nom du chauffeur à modifier : ");
        string nom = Console.ReadLine();
        Console.Write("Nouveau type de permis (A, B, C) : ");
        string permis = Console.ReadLine();
        Console.Write("Nouvelle ancienneté (en années) : ");
        int anciennete;
        int.TryParse(Console.ReadLine(), out anciennete);

        flotte.ModifierChauffeur(nom, permis, anciennete);
        Pause();
    }

    static void SupprimerChauffeur(GestionFlotte flotte)
    {
        Console.Write("Nom du chauffeur à supprimer : ");
        string nom = Console.ReadLine();

        flotte.SupprimerChauffeur(nom);
        Pause();
    }

    // Menu pour la gestion des trajets
    static void MenuTrajets(GestionFlotte flotte)
    {
        if (!flotte.ADesChauffeurs || !flotte.ADesVehicules)
        {
            Console.WriteLine("Vous devez avoir au moins un chauffeur et un véhicule pour gérer les trajets.");
            Pause();
            return;
        }

        bool retour = false;
        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("=== Gestion des Trajets ===");
            Console.WriteLine("1. Ajouter un trajet");
            Console.WriteLine("2. Afficher tous les trajets");
            Console.WriteLine("3. Retour au menu principal");
            Console.Write("Veuillez choisir une option : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AjouterTrajet(flotte);
                    break;
                case "2":
                    flotte.AfficherTrajets();
                    Pause();
                    break;
                case "3":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    Pause();
                    break;
            }
        }
    }

    static void AjouterTrajet(GestionFlotte flotte)
    {
        Console.Write("Lieu de départ : ");
        string depart = Console.ReadLine();
        Console.Write("Lieu d'arrivée : ");
        string arrivee = Console.ReadLine();
        Console.Write("Distance (en km) : ");
        double distance;
        while (!double.TryParse(Console.ReadLine(), out distance) || distance <= 0)
        {
            Console.Write("Veuillez entrer une distance valide (nombre positif) : ");
        }
        Console.Write("Durée estimée (en heures) : ");
        double dureeHeures;
        while (!double.TryParse(Console.ReadLine(), out dureeHeures) || dureeHeures <= 0)
        {
            Console.Write("Veuillez entrer une durée valide (nombre positif) : ");
        }
        TimeSpan duree = TimeSpan.FromHours(dureeHeures);

        Console.Write("Nom du chauffeur : ");
        string nomChauffeur = Console.ReadLine();
        Console.Write("Immatriculation du véhicule : ");
        string immatVehicule = Console.ReadLine();

        var chauffeur = flotte.ObtenirChauffeur(nomChauffeur);
        var vehicule = flotte.ObtenirVehicule(immatVehicule);

        if (chauffeur == null)
        {
            Console.WriteLine("Chauffeur non trouvé.");
            Pause();
            return;
        }

        if (vehicule == null)
        {
            Console.WriteLine("Véhicule non trouvé.");
            Pause();
            return;
        }

        try
        {
            Trajet trajet = new Trajet(depart, arrivee, distance, duree, chauffeur, vehicule);
            flotte.AjouterTrajet(trajet);
            Console.WriteLine("Trajet ajouté avec succès.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout du trajet : {ex.Message}");
        }
        Pause();
    }

    // Menu pour la maintenance des véhicules
    static void MenuMaintenance(GestionFlotte flotte)
    {
        bool retour = false;
        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("=== Maintenance des Véhicules ===");
            Console.WriteLine("1. Effectuer une maintenance");
            Console.WriteLine("2. Afficher les maintenances");
            Console.WriteLine("3. Retour au menu principal");
            Console.Write("Veuillez choisir une option : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    EffectuerMaintenance(flotte);
                    break;
                case "2":
                    flotte.AfficherMaintenances();
                    Pause();
                    break;
                case "3":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez réessayer.");
                    Pause();
                    break;
            }
        }
    }

    static void EffectuerMaintenance(GestionFlotte flotte)
    {
        Console.Write("Immatriculation du véhicule : ");
        string immat = Console.ReadLine();
        Console.Write("Type de maintenance : ");
        string typeMaintenance = Console.ReadLine();
        Console.Write("Coût de la maintenance : ");
        decimal cout;
        decimal.TryParse(Console.ReadLine(), out cout);

        var vehicule = flotte.ObtenirVehicule(immat);

        if (vehicule == null)
        {
            Console.WriteLine("Véhicule non trouvé.");
            Pause();
            return;
        }

        flotte.EffectuerMaintenance(vehicule, typeMaintenance, cout);
        Console.WriteLine("Maintenance effectuée avec succès.");
        Pause();
    }

    // Méthode pour mettre en pause le programme
    static void Pause()
    {
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}
