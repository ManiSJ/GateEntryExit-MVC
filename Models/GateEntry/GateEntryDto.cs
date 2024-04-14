using System.ComponentModel.DataAnnotations;

namespace GateEntryExit_MVC.Models.GateEntry
{
    public class GateEntryDto
    {
        public Guid? Id { get; set; }

        public Guid? GateId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Range(0, 23)]
        public int? Hour { get; set; }

        [Range(0, 59)]
        public int? Minute { get; set; }

        public DateTime TimeStamp { get; set; }

        public int? NumberOfPeople { get; set; }

        public string GateName { get; set; }
    }
}
