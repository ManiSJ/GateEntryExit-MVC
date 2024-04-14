namespace GateEntryExit_MVC.Models.Gate
{
    public class GateCrudWithList
    {
        public GetAllGatesDto Gates { get; set; }

        public GateDto Gate { get; set; }

        public int PageNumber { get; set; }
    }
}
