namespace GateEntryExit_MVC.Models.GateEntry
{
    public class UpdateGateEntryDto
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
