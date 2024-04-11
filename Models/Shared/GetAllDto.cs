namespace GateEntryExit_MVC.Models.Shared
{
    public class GetAllDto
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string Sorting { get; set; }
    }
}
