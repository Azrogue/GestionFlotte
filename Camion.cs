using System;
namespace FlotteVoiture
{
    public class Camion : Vehicule
    {
        public Camion(string immatriculation, string marque, string modele, int kilometrageInitial = 0)
            : base(immatriculation, marque, modele, 30000, kilometrageInitial) {
            Discriminator = "Camion";
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"Camion - {Marque} {Modele}, Immatriculation: {Immatriculation}, Kilométrage: {Kilometrage} km");
        }
        public Camion() { }
    }
}

