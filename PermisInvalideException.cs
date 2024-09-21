using System;
namespace FlotteVoiture
{
    public class PermisInvalideException : Exception
    {
        public PermisInvalideException(string message) : base(message) { }
    }
}

