namespace GateEntryExit_MVC.Models.GateExit
{
    public class GetAllGateExitsDto
    {
        public int TotalCount { get; set; }

        public List<GateExitDto> Items { get; set; }
    }
}
