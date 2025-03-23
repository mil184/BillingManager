namespace Infrastructure.Exceptions
{
    public class BillAmountExceedsLimitException : Exception
    {
        public BillAmountExceedsLimitException(string message) : base(message) { }
    }
}
