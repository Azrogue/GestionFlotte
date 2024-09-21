using System;
namespace FlotteVoiture
{
    public class SuppressionVehiculeAssigneException : Exception
    {
        public SuppressionVehiculeAssigneException(string message) : base(message) { }
    }
}

