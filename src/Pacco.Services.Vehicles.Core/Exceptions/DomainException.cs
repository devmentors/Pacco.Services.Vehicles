using System;

namespace Pacco.Services.Vehicles.Core.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string Code { get; }

        protected DomainException(string message) : base(message)
        {
        }
    }
}