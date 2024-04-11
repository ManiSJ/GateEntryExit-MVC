namespace GateEntryExit_MVC.Models.Shared
{
    public class PaginationModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public int TotalItems { get; set; }
    }
}
