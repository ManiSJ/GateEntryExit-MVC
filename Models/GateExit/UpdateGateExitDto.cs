namespace GateEntryExit_MVC.Models.GateExit
{
    public class UpdateGateExitDto
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int NumberOfPeople { get; set; }
    }
}
