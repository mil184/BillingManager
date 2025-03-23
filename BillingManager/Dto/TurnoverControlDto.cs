namespace Api.Dto
{
    public record TurnoverControlDto
    {
        public double BillUpperLimit { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
