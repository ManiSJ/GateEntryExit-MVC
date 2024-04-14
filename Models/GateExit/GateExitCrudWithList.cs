using GateEntryExit_MVC.Models.GateEntry;

namespace GateEntryExit_MVC.Models.GateExit
{
    public class GateExitCrudWithList
    {
        public GetAllGateExitsDto GateExits { get; set; }

        public GateExitDto GateExit { get; set; }

        public int PageNumber { get; set; }
    }
}
