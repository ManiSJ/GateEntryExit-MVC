namespace GateEntryExit_MVC.Models.GateEntry
{
    public class CreateGateEntryDto
    {
        public Guid GateId { get; set; }

        public DateTime TimeStamp { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
