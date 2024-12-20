// Core/Exceptions/InvalidAmountException.cs
namespace Core.Exceptions
{
    public class InvalidAmountException : DomainException
    {
        public InvalidAmountException() : base("Invalid amount.")
        {
        }

        public InvalidAmountException(string message) : base(message)
        {
        }
    }
}
