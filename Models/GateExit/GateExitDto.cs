using System.ComponentModel.DataAnnotations;

namespace GateEntryExit_MVC.Models.GateExit
{
    public class GateExitDto
    {
        public Guid? Id { get; set; }

        public Guid? GateId { get; set; }

        public DateTime TimeStamp { get; set; }

        public int? NumberOfPeople { get; set; }

        public string GateName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Range(0, 23)]
        public int? Hour { get; set; }

        [Range(0, 59)]
        public int? Minute { get; set; }
    }
}
