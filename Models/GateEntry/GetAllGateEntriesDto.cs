namespace GateEntryExit_MVC.Models.GateEntry
{
    public class GetAllGateEntriesDto
    {
        public int TotalCount { get; set; }

        public List<GateEntryDto> Items { get; set; }
    }
}
