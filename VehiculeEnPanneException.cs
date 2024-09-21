using System;
namespace FlotteVoiture
{
    public class VehiculeEnPanneException : Exception
    {
        public VehiculeEnPanneException(string message) : base(message) { }
    }

}

