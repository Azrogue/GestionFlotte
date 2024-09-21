using System;
namespace FlotteVoiture
{
    public class Voiture : Vehicule
    {
        public Voiture(string immatriculation, string marque, string modele, int kilometrageInitial = 0)
            : base(immatriculation, marque, modele, 15000, kilometrageInitial) {
            Discriminator = "Voiture";
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"Voiture - {Marque} {Modele}, Immatriculation: {Immatriculation}, Kilométrage: {Kilometrage} km");
        }
        public Voiture() { } // Constructeur sans paramètre requis par EF Core
    }

}

