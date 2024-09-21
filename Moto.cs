using System;
namespace FlotteVoiture
{
    public class Moto : Vehicule
    {
        public Moto(string immatriculation, string marque, string modele, int kilometrageInitial = 0)
            : base(immatriculation, marque, modele, 10000, kilometrageInitial) {
            Discriminator = "Moto";
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"Moto - {Marque} {Modele}, Immatriculation: {Immatriculation}, Kilométrage: {Kilometrage} km");
        }
        public Moto() { }
    }

}

