namespace GateEntryExit_MVC.Models.GateEntry
{
    public class GateEntryCrudWithList
    {
        public GetAllGateEntriesDto GateEntries { get; set; }

        public GateEntryDto GateEntry { get; set; }

        public int PageNumber { get; set; }
    }
}
