using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlotteVoiture
{
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; } // Clé primaire générée automatiquement

        public DateTime DateMaintenance { get; set; }
        public string TypeMaintenance { get; set; }
        public decimal Cout { get; set; }

        // Clé étrangère et navigation property
        public int VehiculeId { get; set; }
        public Vehicule VehiculeConcerne { get; set; }

        public Maintenance(DateTime date, string type, decimal cout, Vehicule vehicule)
        {
            DateMaintenance = date;
            TypeMaintenance = type;
            Cout = cout;
            VehiculeConcerne = vehicule;
        }

        public void AfficherDetails()
        {
            Console.WriteLine($"Maintenance du {DateMaintenance.ToShortDateString()} - Type: {TypeMaintenance}, Coût: {Cout} €, Véhicule: {VehiculeConcerne.Immatriculation}");
        }
        public Maintenance() { }
    }
}

