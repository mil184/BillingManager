namespace Infrastructure.Exceptions
{
    public class BillIsNullException : Exception
    {
        public BillIsNullException(string message) : base(message) { }
    }
}
